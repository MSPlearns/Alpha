using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            //return LocalRedirect("/projects");
            var viewModel = new RegisterViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Register(UserRegisterForm formData)
        {
            if (!ModelState.IsValid)
            {
                RegisterViewModel viewModel = new()
                {
                    FormData = formData
                };
                return View(viewModel);
            }

            return View();

            //return LocalRedirect("/projects");
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
