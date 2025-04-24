using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class ProjectEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }

    //Foreign keys 
    [ForeignKey(nameof(ProjectStatus))]
    public int StatusId { get; set; }
    public StatusEntity ProjectStatus { get; set; } = null!;

    [ForeignKey(nameof(ProjectManager))]
    public string ProjectManagerId { get; set; } = null!;
    public AppUserEntity ProjectManager { get; set; } = null!;

    [ForeignKey(nameof(ProjectClient))]
    public string ClientId { get; set; } = null!;
    public ClientEntity ProjectClient { get; set; } = null!;
}