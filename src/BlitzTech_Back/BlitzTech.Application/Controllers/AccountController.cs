using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlitzTech.Domain.DTOs.LoginDTO;
using BlitzTech.Domain.DTOs.RegisterDTO;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, ITokenService tokenService, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.UserName.ToLower());

            if (user == null)
                return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid password!");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new NewUserDTO
            {
                UserName = user.UserName,
                EmailAddress = user.Email,
                Token = _tokenService.CreateToken(user, roles)
            });
        }

        private async Task<bool> IsAdminUser()
        {
            var user = await _userManager.GetUserAsync(User);
            return user != null && user.UserName == "Admin";
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("admindata")]
        public async Task<IActionResult> GetAdminData()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || currentUser.UserName != "Admin")
            {
                return Forbid("Only the user with username 'Admin' can access this endpoint.");
            }

            var users = _userManager.Users.Select(u => new { u.UserName, u.Email }).ToList();
            return Ok(users);
        }


        [Authorize]
        [HttpPost("Register-ADM")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO registerDto)
        {
            if (!await IsAdminUser())
            {
                return Forbid("Only the user with username 'Admin' can access this endpoint.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.EmailAddress
            };

            var result = await _userManager.CreateAsync(user, registerDto.PassWord);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                if (roleResult.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return Ok(new NewUserDTO
                    {
                        UserName = user.UserName,
                        EmailAddress = user.Email,
                        Token = _tokenService.CreateToken(user, roles)
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

        [Authorize]
        [HttpPost("Test")]
        public async Task<IActionResult> SomeOtherAction()
        {
            if (!await IsAdminUser())
            {
                return Forbid("Only the user with username 'Admin' can access this endpoint.");
            }
            return Ok("Ação executada com sucesso!");
        }
    }
}
