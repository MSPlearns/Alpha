using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignInViewModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "Enter your email")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]

    [Display(Name = "Password", Prompt = "Enter a secure password")]

    public string Password { get; set; } = null!;

    public bool IsPersistent { get; set; }
}
