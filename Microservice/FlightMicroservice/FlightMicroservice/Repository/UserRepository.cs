using FlightMicroservice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightMicroservice.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> CreateUserAsync(RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return user;
            }

            throw new Exception("Failed to create user");
        }

        public async Task<ApplicationUser> UpdateUserAsync(string userId, UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.Email = model.Email ?? user.Email;
            user.PasswordHash = model.Password ?? user.PasswordHash;
            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            await _userManager.DeleteAsync(user);
        }

        public async Task AddRoleToUserAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new Exception("Role does not exist");
            }

            await _userManager.AddToRoleAsync(user, role);
        }
       
        public async Task RemoveRoleFromUserAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            await _userManager.RemoveFromRoleAsync(user, role);
        }


        public async Task AddPermissionToRoleAsync(PermissionRequest model)
        {
            var role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
                throw new Exception("Role not found");

            var claim = new Claim("Permission", model.Permission);
            await _roleManager.AddClaimAsync(role, claim);
        }
        public async Task DeactivateUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.IsActive = false; 
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to deactivate user.");
            }
        }
        public async Task ActivateUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.IsActive)
            {
                throw new Exception("User is already active.");
            }

            user.IsActive = true; // Kích hoạt tài khoản
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to activate user.");
            }
        }


        //public async Task RemovePermissionFromUserAsync(string userId, string permission)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        throw new Exception("User not found");

        //    var claims = await _userManager.GetClaimsAsync(user);
        //    var claimToRemove = claims.FirstOrDefault(c => c.Type == "Permission" && c.Value == permission);

        //    if (claimToRemove != null)
        //        await _userManager.RemoveClaimAsync(user, claimToRemove);
        //}

        //public async Task AddPermissionToUserAsync(string userId, string permission)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        throw new Exception("User not found");

        //    await _userManager.AddClaimAsync(user, new Claim("Permission", permission));
        //}
    }
}
