using Business.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

    [Route("admin")]
public class AdminController : Controller
{
    [Route("members")]
    public IActionResult Members()
    {
        return View();
    }

    [Route("clients")]
    public IActionResult Clients()
    {
        var viewModel = new AddClientViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AddClient(AddClientForm formData)
    {
        if (!ModelState.IsValid)
        {
            AddClientViewModel viewModel = new()
            {
                FormData = formData
            };
            return RedirectToAction("Clients");
        }

        return View();
    }
    [HttpPost]
    public IActionResult EditClient(AddClientForm formData)
    {
        if (!ModelState.IsValid)
        {
            AddClientViewModel viewModel = new()
            {
                FormData = formData
            };
            return RedirectToAction("Clients");
        }

        return View();
    }
}
