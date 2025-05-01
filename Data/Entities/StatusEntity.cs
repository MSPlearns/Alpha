using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int Id { get; set; } = 0;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Colour { get; set; } = "grey";

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
