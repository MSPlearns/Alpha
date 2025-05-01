
namespace Domain.Models;

public class Status
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Colour { get; set; } = "grey";
}
