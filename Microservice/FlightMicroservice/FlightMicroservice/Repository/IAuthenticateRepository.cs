using FlightMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FlightMicroservice.Repository
{
    public interface IAuthenticateRepository
    {
        Task<IActionResult> LoginAsync(LoginModel model);
        Task<IActionResult> RegisterAsync(RegisterModel model);
        Task<IActionResult> RegisterAdminAsync(RegisterModel model);
        Task<IActionResult> RefreshTokenAsync(TokenModel tokenModel);
        Task<IActionResult> RevokeAsync(string email);
        Task<IActionResult> RevokeAllAsync();
        Task<IActionResult> LogoutAsync(string email);
    }

}
