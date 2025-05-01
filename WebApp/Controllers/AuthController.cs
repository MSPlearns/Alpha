using Business.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            ViewBag.ErrorMessage = null;
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var signUpFormData =  viewModel.MapTo<SignUpFormData>();
            var result = await _authService.SignUpAsync(signUpFormData);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(viewModel);
        }

        public IActionResult SignIn(string returnurl = "~/")
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnurl = "~/")
        {
            ViewBag.ErrorMessage = null;
            ViewBag.ReturnUrl = returnurl;
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var signInFormData = viewModel.MapTo<SignInFormData>();
            var result = await _authService.SignInAsync(signInFormData);
            if (result.Succeeded)
            {
                return LocalRedirect(returnurl);
            }
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(viewModel);
        }
    }
}
