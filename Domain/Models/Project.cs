
namespace Domain.Models;

public class Project
{

    public string Id { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public string? ProjectImage { get; set; }
    public string? Description { get; set; }
    public decimal? ProjectBudget { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime CreatedDate { get; set; } 

    public DateTime? EndDate { get; set; }

    //Foreign keys 

    public Status Status { get; set; } = null!;

    public AppUser ProjectManagerId { get; set; } = null!;

    public Client Client { get; set; } = null!;

}
