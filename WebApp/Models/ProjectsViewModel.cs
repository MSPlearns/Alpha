using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ProjectsViewModel
{
    public IEnumerable<ProjectViewModel> ProjectList { get; set; } = [];
    public AddProjectViewModel AddProjectFormData { get; set; } = new();
    public EditProjectViewModel EditProjectFormData { get; set; } = new();
}

public class ProjectViewModel
{
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;

    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public DateTime? EndDate { get; set; }
    public string TimeLeft 
    {
        get
        {
            return CalculateTimeLeft();
        }
    }

    public string TimeLeftColour { get; private set; } = "grey";


    //Foreign keys 

    public StatusViewModel ProjectStatus { get; set; } = null!;

    public AppUserViewModel ProjectManager { get; set; } = null!;

    public ClientViewModel ProjectClient { get; set; } = null!;

    public string CalculateTimeLeft()
    {
        if (EndDate == null)
            return "Unknown";

        var daysLeft = (EndDate.Value.Date - DateTime.Today).Days;

        if (daysLeft < 0)
            return "No time left";

        if (daysLeft < 7)
            return $"{daysLeft}d";

        int weeks = daysLeft / 7;
        int days = daysLeft % 7;

        TimeLeftColour = SetTimeLeftColour(daysLeft);

        return days > 0 ? $"{weeks}w {days}d" : $"{weeks}w";
    }

    public string SetTimeLeftColour(int daysLeft)
    {
        switch (daysLeft)
        { 
            case < 0:
                return "red";
            case < 7:
                return "orange";
            case < 14:
                return "yellow";
            case < 21:
                return "green";
            default:
                return "grey";
        }
    }
}

public class AddProjectViewModel
{
    public IEnumerable<SelectListItem> ClientList { get; set; } = [];
    public IEnumerable<SelectListItem> AppUserList { get; set; } = [];
    public IEnumerable<SelectListItem> StatusList { get; set; } = [];

    [Display(Name = "Title", Prompt = "Enter the projects title")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2 to 50 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }

    [Display(Name = "Description", Prompt = "Describe the project...")]
    [DataType(DataType.Text)]
    [StringLength(250, MinimumLength = 10, ErrorMessage = "2 to 250 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    public string? Description { get; set; }

    [Display(Name = "Budget", Prompt = "Enter the budget")]
    [RegularExpression(@"^\d{1,18}(,\d{1,2})?$", ErrorMessage = "Invalid format - use 0000,00")]
    [Range(0, double.MaxValue, ErrorMessage = "Budget must be a non-negative value")]
    public decimal? Budget { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? StartDate { get; set; }

    //Foreign keys 

    [Required(ErrorMessage = "Required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid status.")]
    public int StatusId { get; set; } = 0;


    [Required(ErrorMessage = "Required")]
    public string ProjectManagerId { get; set; } = null!;


    [Required(ErrorMessage = "Required")]
    public string ClientId { get; set; } = null!;

}

public class EditProjectViewModel
{
    public IEnumerable<SelectListItem> ClientList { get; set; } = [];
    public IEnumerable<SelectListItem> AppUserList { get; set; } = [];
    public IEnumerable<SelectListItem> StatusList { get; set; } = [];

    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? StartDate { get; set; }

    //Foreign keys 

    public int StatusId { get; set; } = 0;

    public string ProjectManagerId { get; set; } = null!;

    public string ClientId { get; set; } = null!;
}

