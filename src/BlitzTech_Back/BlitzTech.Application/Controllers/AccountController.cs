using BlitzTech.Data.Context;
using BlitzTech.Domain.DTOs.LoginDTO;
using BlitzTech.Domain.DTOs.RegisterDTO;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace api.Controllers
{

    //     {
    //   "userName": "Admin",
    //   "emailAddress": "user@example.com",
    //   "passWord": "Teste_12345678"
    // }

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IUnitOfWork> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<IUnitOfWork> _signinManager;

        public AccountController(UserManager<IUnitOfWork> userManager, ITokenService tokenService, SignInManager<IUnitOfWork> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.UserName.ToLower());

            if (user == null)
                return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid password!");

            return Ok(new NewUserDTO
            {
                UserName = user.UserName,
                EmailAddress = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new IUnitOfWork
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.EmailAddress
                };

                var result = await _userManager.CreateAsync(user, registerDto.PassWord);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDTO
                        {
                            UserName = user.UserName,
                            EmailAddress = user.Email,
                            Token = _tokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
