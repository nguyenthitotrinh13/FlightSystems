// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using FlightSystem.Services;
using FlightSystem.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var user = _authService.Authenticate(model.Username, model.Password);

        if (user == null)
        {
            return Unauthorized();
        }
        return Ok(new { message = "Login successful", user });
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
