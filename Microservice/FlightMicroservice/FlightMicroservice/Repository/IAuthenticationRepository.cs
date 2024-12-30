using FlightMicroservice.Models;

namespace FlightMicroservice.Repository
{
    public interface IAuthenticationRepository
    {
        Task<(string Token, string RefreshToken)> AuthenticateUserAsync(string email, string password);
        Task<bool> RegisterUserAsync(RegisterModel model);
        Task<bool> RevokeTokenAsync(string email);
    }
}
