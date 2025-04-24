namespace Domain.Dtos;

public class AddProjectFormData
{
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    //Foreign keys 

    public int StatusId { get; set; } = 0;

    public string ProjectManagerId { get; set; } = null!;

    public string ClientId { get; set; } = null!;
}

public class EditProjectFormData
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    //Foreign keys 

    public int StatusId { get; set; } = 0;

    public string ProjectManagerId { get; set; } = null!;

    public string ClientId { get; set; } = null!;
}




