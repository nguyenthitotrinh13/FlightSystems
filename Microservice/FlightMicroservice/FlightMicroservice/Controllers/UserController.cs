using FlightMicroservice.Models;
using FlightMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "CanManageUsers")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userManagementService)
        {
            _userRepository = userManagementService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterModel model)
        {
            var user = await _userRepository.CreateUserAsync(model);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserModel model)
        {
            var user = await _userRepository.UpdateUserAsync(id, model);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("add-role/{userId}")]
        public async Task<IActionResult> AddRoleToUserAsync(string userId, [FromBody] string role)
        {
            await _userRepository.AddRoleToUserAsync(userId, role);
            return NoContent();
        }

        [HttpPost("remove-role/{userId}")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, [FromBody] string role)
        {
            await _userRepository.RemoveRoleFromUserAsync(userId, role);
            return NoContent();
        }
        [HttpPost("add-permission-to-role")]
        public async Task<IActionResult> AddPermissionToRole([FromBody] PermissionRequest model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.RoleName) || string.IsNullOrEmpty(model.Permission))
                {
                    return BadRequest(new { Message = "RoleName and Permission are required." });
                }
                await _userRepository.AddPermissionToRoleAsync(model);

                return Ok(new { Message = "Permission added to role successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            try
            {
                await _userRepository.DeactivateUserAsync(id);
                return Ok(new { Message = "User deactivated successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateUser(string id)
        {
            try
            {
                await _userRepository.ActivateUserAsync(id);
                return Ok(new { Message = "User activated successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        //[HttpPost("remove-permission/{userId}")]
        //public async Task<IActionResult> RemovePermission(string userId, [FromBody] PermissionRequest model)
        //{
        //    try
        //    {
        //        // Sử dụng model.Permission để lấy quyền cần xóa
        //        await _userRepository.RemovePermissionFromUserAsync(userId, model.Permission);
        //        return Ok(new { Message = "Permission removed successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = ex.Message });
        //    }

        //}

    }
}
