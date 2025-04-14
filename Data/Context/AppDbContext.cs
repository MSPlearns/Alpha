using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUserEntity>(options)
{
    public virtual DbSet<ClientEntity> Clients { get; set; } = null!;
    public virtual DbSet<StatusEntity> Statuses { get; set; } = null!;
    public virtual DbSet<ProjectEntity> Projects { get; set; } = null!;
}
