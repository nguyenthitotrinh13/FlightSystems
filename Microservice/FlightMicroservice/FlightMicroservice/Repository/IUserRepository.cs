using FlightMicroservice.Models;
using Microsoft.AspNetCore.Identity;

namespace FlightMicroservice.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> CreateUserAsync(RegisterModel model);
        Task<ApplicationUser> UpdateUserAsync(string userId, UpdateUserModel model);
        Task DeleteUserAsync(string userId);
        Task AddRoleToUserAsync(string userEmail, string roleName);
        Task RemoveRoleFromUserAsync(string userId, string role);
        Task AddPermissionToRoleAsync(PermissionRequest model);
        Task DeactivateUserAsync(string userId);
        Task ActivateUserAsync(string userId);
        
        //Task RemovePermissionFromUserAsync(string userId, string permission);
    }
}
