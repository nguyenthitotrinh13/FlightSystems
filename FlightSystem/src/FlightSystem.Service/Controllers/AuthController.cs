using FlightSystem.Service.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Service.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private static readonly List<UserDTO> users = new()
    {
        new UserDTO(Guid.NewGuid(), "trinh", "password", "admin"),
        new UserDTO(Guid.NewGuid(), "trinh", "123456", "user"),
    };
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO login)
    {
        var user = users.SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);

        if (user == null)
        {
            return Unauthorized(new { Message = "Username or password is not correct!" });
        }

        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        return Ok(new { Token = token, Role = user.Role });
    }
    }
}