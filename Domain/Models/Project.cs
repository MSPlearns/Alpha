
namespace Domain.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime CreatedDate { get; set; } 

    public DateTime? EndDate { get; set; }

    //Foreign keys 

    public Status ProjectStatus { get; set; } = null!;

    public AppUser ProjectManager { get; set; } = null!;

    public Client ProjectClient { get; set; } = null!;

}
