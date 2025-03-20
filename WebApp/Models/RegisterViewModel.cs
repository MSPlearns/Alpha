using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class RegisterViewModel
{
    public UserRegisterForm FormData { get; set; } = new();
    public List<SelectListItem> ClientOptions { get; set; } = [];

    public RegisterViewModel()
    {
        ClientOptions =
        [
            new() { Value = "1", Text = "Client 1" },
            new() { Value = "2", Text = "Client 2" },
            new() { Value = "3", Text = "Client 3" },
            new() { Value = "4", Text = "Client 4" }
        ];

    }

    //public async Task PopulateClientsAsync()
    //{
    //    // This is where you would query the database for the clients
    //    // and populate the Clients property with the results

    //}
}
