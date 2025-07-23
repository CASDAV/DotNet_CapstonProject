using LogiTrack.API.Entities;
using LogiTrack.API.Services;
using LogiTrack.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityServices _identityServices;
        private readonly TokenService _tokenService;

        public AuthController(IIdentityServices identityServices, TokenService tokenService)
        {
            _identityServices = identityServices;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("The main values (User Name, Email, Pasword) for register a new user can not be empty or white spaces.");

            var result = string.IsNullOrWhiteSpace(user.Role)
                ? await _identityServices.RegisterUser(user.UserName, user.Email, user.Password)
                : await _identityServices.RegisterUser(user.UserName, user.Email, user.Password, user.Role);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.UserName) || string.IsNullOrWhiteSpace(loginRequest.Password)) return BadRequest("Empty Credentials");

            var result = await _identityServices.Login(loginRequest.UserName, loginRequest.Password);

            if (!result.loginResult) return Unauthorized("Invalid Credentials");

            var token = _tokenService.GenerateToken(loginRequest.UserName, result.userRole);

            return Ok(new { Token = token });
        }

        [HttpGet("create-role/{roleName}")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) return BadRequest("Invalid role Name");

            var result = await _identityServices.CreateRole(roleName);

            return Ok(result);
        }
    }
}
