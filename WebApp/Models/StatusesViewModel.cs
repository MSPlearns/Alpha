using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class StatusesViewModel
{
    public IEnumerable<StatusViewModel> StatusList { get; set; } = [];
    public AddStatusViewModel AddStatusFormData { get; set; } = new();
    public EditStatusViewModel EditStatusFormData { get; set; } = new();
}

public class StatusViewModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Colour { get; set; }
}

public class AddStatusViewModel
{
    public IEnumerable<SelectListItem> ColourList { get; set; } = [];
    public AddStatusViewModel() //There is probably a better way to do this than manually
    {
        ColourList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Grey", Text = "Grey" },
            new SelectListItem { Value = "Green", Text = "Green" },
            new SelectListItem { Value = "Yellow", Text = "Yellow" },
            new SelectListItem { Value = "Red", Text = "Red" },
            new SelectListItem { Value = "Blue", Text = "Blue" }
        };
    }

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2 to 50 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Name", Prompt = "Enter status name")]
    public string Name { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "2 to 150 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Description", Prompt = "Enter a description")]
    public string? Description { get; set; }
    public string? Colour { get; set; }
}

public class EditStatusViewModel
{
    public IEnumerable<SelectListItem> ColourList { get; set; } = [];
    public EditStatusViewModel() //There is probably a better way to do this than manually
    {
        ColourList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Grey", Text = "Grey" },
            new SelectListItem { Value = "Green", Text = "Green" },
            new SelectListItem { Value = "Yellow", Text = "Yellow" },
            new SelectListItem { Value = "Red", Text = "Red" },
            new SelectListItem { Value = "Blue", Text = "Blue" }
        };
    }

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2 to 50 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Name", Prompt = "Enter status name")]
    public string Name { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "2 to 150 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Description", Prompt = "Enter a description")]
    public string? Description { get; set; }
    public string? Colour { get; set; }
}