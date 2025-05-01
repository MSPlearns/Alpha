using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AppUsersAdminViewModel
{
    public IEnumerable<AppUserViewModel> AppUserList { get; set; } = [];
    public EditAppUserViewModel EditAppUserViewModel { get; set; } = new();
    public SignUpViewModel AddAppUserViewModel { get; set; } = new();
    //The admin uses signupviewmodel to add a new user
}
public class AppUsersViewModel //For logged in users (not  admins)
{
    public IEnumerable<AppUserViewModel> AppUserList { get; set; } = [];
}

public class AppUserViewModel
{
    public string? ImageUrl { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? JobTitle { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string ProfileUrl => $"/user/{FirstName}{LastName}"; //ChatGPT helped with this one, as "=" on its own was not working
}

public class EditAppUserViewModel
{
    // TODO: Add a way to submit a user image
    // TODO: Add user role

    [Display(Name = "First Name", Prompt = "*Enter your first name")]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2-50 characters")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "Invalid characters")]
    public string FirstName { get; set; } = null!;


    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2-50 characters")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Last Name", Prompt = "*Enter your last name")]
    public string LastName { get; set; } = null!;

    [StringLength(50, MinimumLength = 2, ErrorMessage = "2-50 characters")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Last Name", Prompt = "Enter a job title")]
    public string? JobTitle { get; set; }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "*Enter your email")]
    public string Email { get; set; } = null!;


    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Invalid phone number")]
    [Display(Name = "Phone Number", Prompt = "Enter your phone number")]
    public string? PhoneNumber { get; set; }


    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Password", Prompt = "*Enter a secure password")]
    public string Password { get; set; } = null!;


    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [Compare((nameof(Password)), ErrorMessage = "Passwords do not match")]
    [Display(Name = "Confirm Password", Prompt = "*Confirm your password")]
    public string ConfirmPassword { get; set; } = null!;


    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the term and conditions")]
    public bool TermsAndConditionsAccepted { get; set; }
}
