﻿using Microsoft.AspNetCore.Identity;
namespace FlightMicroservice.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
