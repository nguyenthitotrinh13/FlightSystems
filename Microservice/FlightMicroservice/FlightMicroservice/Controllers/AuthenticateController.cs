using FlightMicroservice.Models;
using FlightMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FlightMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateRepository _authenticateRepository;

        public AuthenticateController(IAuthenticateRepository authenticateService)
        {
            _authenticateRepository = authenticateService;
        }

        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return _authenticateRepository.LoginAsync(model);
        }

        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            return _authenticateRepository.RegisterAsync(model);
        }

        [HttpPost("register-admin")]
        public Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            return _authenticateRepository.RegisterAdminAsync(model);
        }

        [HttpPost("refresh-token")]
        public Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            return _authenticateRepository.RefreshTokenAsync(tokenModel);
        }

        [HttpPost("revoke/{email}")]
        public Task<IActionResult> Revoke(string email)
        {
            return _authenticateRepository.RevokeAsync(email);
        }

        [HttpPost("revoke-all")]
        public Task<IActionResult> RevokeAll()
        {
            return _authenticateRepository.RevokeAllAsync();
        }

        [HttpPost("logout")]
        public Task<IActionResult> Logout()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return _authenticateRepository.LogoutAsync(email);
        }

       


    }
}

