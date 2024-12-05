namespace FlightSystem.Service.DTOs
{
    public record LoginDTO(string Username, string Password);
    public record UserDTO(Guid Id, string Username, string Password, string Role);


}