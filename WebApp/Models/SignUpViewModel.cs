using Domain.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignUpViewModel
{
    [Display(Name = "First Name", Prompt = "*Enter first name")]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2-50 characters")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "Invalid characters")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last Name", Prompt = "*Enter last name")]
    [Required(ErrorMessage = "Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "2-50 characters")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "Invalid characters")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "*Enter a valid email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;


    [Display(Name = "Phone Number", Prompt = "Enter a phone number")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "2-50 characters")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "Invalid characters")]
    [Display(Name = "Last Name", Prompt = "Enter a job title")]
    public string? JobTitle { get; set; }


    [Display(Name = "Password", Prompt = "*Enter a secure password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm Password", Prompt = "*Confirm password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Required")]
    [Compare((nameof(Password)), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;


    [Range(typeof(bool),"true", "true", ErrorMessage ="You must accept the term and conditions")]
    public bool TermsAndConditionsAccepted { get; set; }
}
