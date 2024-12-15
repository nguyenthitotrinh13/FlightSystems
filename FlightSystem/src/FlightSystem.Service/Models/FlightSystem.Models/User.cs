public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } // Password should be hashed
    public string Role { get; set; } // E.g., "Admin" or "User"
}