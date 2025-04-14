using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

[Index(nameof(StatusName), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int Id { get; set; } = 0;
    public string StatusName { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
