using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;
public class EditClientFormData
{
    public int Id { get; set; }

    [Display(Name = "Client Image", Prompt = "Select an image")]
    [DataType(DataType.Upload)]
    public IFormFile? ClientImage { get; set; }

    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Client Name", Prompt = "Enter client's name")]
    public string ClientName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email")]
    [Display(Name = "Email", Prompt = "Enter client's email")]
    public string ClientEmail { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number", Prompt = "Enter client's phone number")]
    public string? ClientPhone { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Location", Prompt = "Enter client's location")]
    public string? ClientLocation { get; set; }

    [Required(ErrorMessage = "Required")]
    [Display(Name = "Status")]
    public string ClientStatus { get; set; } = null!;
}