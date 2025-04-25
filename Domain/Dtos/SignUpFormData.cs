using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Domain.Dtos;
public class SignUpFormData
{
    [Display(Name = "First Name", Prompt = "Enter your first name")]
    [Required(ErrorMessage = "You must enter your first name")]
    public string FirstName { get; set; } = null!;
    [Required(ErrorMessage = "You must enter your last name")]
    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "You must enter a valid email")]
    [EmailAddress(ErrorMessage = "You must enter a valid email")]
    [Display(Name = "Email", Prompt = "Enter your email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You must enter a password")]
    [Display(Name = "Password", Prompt = "Enter a secure password")]

    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "You must confirm your password")]
    [Compare((nameof(Password)), ErrorMessage = "Passwords do not match")]
    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]

    public string ConfirmPassword { get; set; } = null!;


    [Required(ErrorMessage = "You must select a client")]
    [Display(Name = "Select a client", Prompt = "Select a client")]
    public int ClientId { get; set; }
}

