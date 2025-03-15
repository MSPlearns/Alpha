using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            return LocalRedirect("/projects");
            //return View();
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
