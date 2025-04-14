using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class ProjectEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectName { get; set; } = null!;
    public string? ProjectImage { get; set; }
    public string? Description { get; set; }
    public decimal? ProjectBudget { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }

    //Foreign keys 
    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

    [ForeignKey(nameof(ProjectManager))]
    public string ProjectManagerId { get; set; } = null!;
    public AppUserEntity ProjectManager { get; set; } = null!;

    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; } = null!;
    public ClientEntity Client { get; set; } = null!;
}