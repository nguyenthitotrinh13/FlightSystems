using System.ComponentModel.DataAnnotations;

namespace FlightMicroservice.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [DomainEmailValidation]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
    public class DomainEmailValidationAttribute : ValidationAttribute
    {
        private readonly string _allowedDomain = "@vietjetair.com";

        public DomainEmailValidationAttribute() : base("Email must have the domain @vietjetair.com")
        {
        }

        public override bool IsValid(object? value)
        {
            if (value is string email && !string.IsNullOrEmpty(email))
            {
                return email.EndsWith(_allowedDomain, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
