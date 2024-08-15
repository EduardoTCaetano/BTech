using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlitzTech.Domain.DTOs.LoginDTO;
using BlitzTech.Domain.DTOs.RegisterDTO;
using BlitzTech.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace api.Controllers
{
    [Route("api/[controller]")]
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

            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);

            if (user == null)
                return Unauthorized("Invalid email address!");

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

        [HttpGet("admindata")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAdminData()
        {
            var users = _userManager.Users.Select(u => new { u.UserName, u.Email }).ToList();
            return Ok(users);
        }

        [HttpPost("Register-ADM")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO registerDto)
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

        [HttpPost("Test")]
        [Authorize]
        public async Task<IActionResult> SomeOtherAction()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return Forbid();
            }
            return Ok("Action executed successfully!");
        }
        
        // [HttpDelete("Delete/{userName}")]
        // public async Task<IActionResult> DeleteUser(string userName)
        // {
        //     var user = await _userManager.FindByNameAsync(userName.ToLower());

        //     if (user == null)
        //         return NotFound("User not found!");

        //     var result = await _userManager.DeleteAsync(user);

        //     if (result.Succeeded)
        //         return Ok("User deleted successfully!");

        //     return StatusCode(500, result.Errors);
        // }
    }
}
