using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUserEntity>(options)
{
    public virtual DbSet<ClientEntity> Clients { get; set; } = null!;
    public virtual DbSet<StatusEntity> Statuses { get; set; } = null!;
    public virtual DbSet<ProjectEntity> Projects { get; set; } = null!;

    //AI generated method, asked for a OnModelCreating method that prevented cascade delete. 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Prevent cascade delete for Project - Status
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.ProjectStatus)
            .WithMany(s => s.Projects)
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Prevent cascade delete for Project - ProjectManager
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.ProjectManager)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.ProjectManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Prevent cascade delete for Project - Client
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.ProjectClient)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
