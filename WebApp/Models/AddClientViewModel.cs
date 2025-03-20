using Business.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class AddClientViewModel
{
    public AddClientForm FormData { get; set; } = new();
    public List<SelectListItem> StatusOptions { get; set; } = new();
    public AddClientViewModel()
    {
        StatusOptions =
        [
            new() { Value = "Active", Text = "Active" },
            new() { Value = "Inactive", Text = "Inactive" },
            new() { Value = "Pending", Text = "Pending" },
            new() { Value = "Backlisted", Text = "Blacklisted" },
        ];
    }
}
