using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BlitzTech.Domain.DTOs.LoginDTO;
using BlitzTech.Domain.DTOs.RegisterDTO;
using BlitzTech.Domain.Interfaces;

namespace BTech.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
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
                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.CreateToken(user, roles);

                var response = new
                {
                    UserName = user.UserName,
                    EmailAddress = user.Email,
                    Token = token
                };

                return Ok(response);
            }

            return StatusCode(500, result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);

            if (user == null)
                return Unauthorized("Invalid email address!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid password!");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user, roles);

            var response = new
            {
                UserName = user.UserName,
                EmailAddress = user.Email,
                Token = token
            };

            return Ok(response);
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(u => new
            {
                u.UserName,
                u.Email
            }).ToList();

            return Ok(users);
        }

        // [HttpGet("GetActiveUsers")]
        // [Authorize(Roles = "Admin")]
        // public IActionResult GetActiveUsers()
        // {
        //     var activeUsers = _userManager.Users.Where(u => u.IsActive).Select(u => new
        //     {
        //         u.UserName,
        //         u.Email
        //     }).ToList();

        //     return Ok(activeUsers);
        // }

        [HttpGet("{userName}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return NotFound("User not found!");

            var response = new
            {
                UserName = user.UserName,
                EmailAddress = user.Email
            };

            return Ok(response);
        }
    }
}
