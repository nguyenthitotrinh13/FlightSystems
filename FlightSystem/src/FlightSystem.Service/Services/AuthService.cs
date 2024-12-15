// Services/AuthService.cs
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FlightSystem.Data;
using FlightSystem.Models;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User Authenticate(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        if (user == null) return null;

        if (!VerifyPasswordHash(password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }

    private bool VerifyPasswordHash(string password, string storedHash)
    {
        using (var hmac = new HMACSHA512())
        {
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }
    }
}
