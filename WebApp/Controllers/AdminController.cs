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
        return View();
    }
}

