using Domain.Models;

namespace Domain.Dtos;

public class AddStatusFormData
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Colour { get; set; } = "grey";

}

public class EditStatusFormData
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Colour { get; set; } = "grey";

}




