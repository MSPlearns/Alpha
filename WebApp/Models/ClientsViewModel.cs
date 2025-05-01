using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ClientsViewModel
{
    public IEnumerable<ClientViewModel> ClientList { get; set; } = [];
    public AddClientViewModel AddClientFormData { get; set; } = new();
    public EditClientViewModel EditClientFormData { get; set; } = new();
}


public class ClientViewModel
{
    //Do i need id?
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}

public class AddClientViewModel
{
    [Display(Name = "Name", Prompt = "Enter client's name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2 to 50 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    public string Name { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "Enter client's email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Invalid phone number")]
    [Display(Name = "Phone Number", Prompt = "Enter client's phone number")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2 to 50 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Location", Prompt = "Enter client's location")]
    public string Location { get; set; } = null!;
}

public class EditClientViewModel
{
    public int Id { get; set; }

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2 to 50 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]

    [Display(Name = "Client Name", Prompt = "Enter client's name")]
    public string Name { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "Enter client's email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Invalid phone number")]
    [Display(Name = "Phone Number", Prompt = "Enter client's phone number")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2 to 50 characters")]
    [RegularExpression(@"^[\p{L}\p{N}\s\-\.&]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Location", Prompt = "Enter client's location")]
    public string Location { get; set; } = null!;
}