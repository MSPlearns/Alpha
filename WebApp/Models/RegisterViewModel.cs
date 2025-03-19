using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class RegisterViewModel
{
    public RegisterFormModel FormData { get; set; } = new();
    public List<SelectListItem> ClientOptions{ get; set; } = [];

    public RegisterViewModel()
    {

    }

        public async Task PopulateClientsAsync()
    {
        // This is where you would query the database for the clients
        // and populate the Clients property with the results

    }
}
