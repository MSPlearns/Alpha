
namespace Domain.Models;

public class AppUser
{
    public string Id { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? JobTitle { get; set; }
    public string? PhoneNumber { get; set; }

    //TODO: Add role
}
