using Microsoft.AspNetCore.Identity;

namespace Data.Entities;
public class AppUserEntity : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
