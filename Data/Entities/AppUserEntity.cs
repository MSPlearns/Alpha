using Microsoft.AspNetCore.Identity;

namespace Data.Entities;
public class AppUserEntity : IdentityUser
{
    public string? ImageUrl { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? JobTitle { get; set; }


    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
