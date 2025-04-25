using Data.Entities;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Results;

namespace Business.Services;

public interface IAuthService
{
    Task<Result<AppUser>> SignInAsync(SignInFormData formData);
    Task SignOutAsync();
    Task<Result<AppUser>> SignUpAsync(SignUpFormData formData);
}

public class AuthService(UserManager<AppUserEntity> userManager, IAppUserService appUserService, SignInManager<AppUserEntity> signInManager) : IAuthService
{
    private readonly UserManager<AppUserEntity> _userManager = userManager;

    private readonly IAppUserService _appUserService = appUserService;

    private readonly SignInManager<AppUserEntity> _signInManager = signInManager;




    public async Task<Result<AppUser>> SignInAsync(SignInFormData formData)
    {
        if (string.IsNullOrEmpty(formData.Email) || string.IsNullOrEmpty(formData.Password))
        {
            return Result<AppUser>.BadRequest("Email and password are required.");
        }

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, false);

        return result.Succeeded
             ? Result<AppUser>.Ok()
             : Result<AppUser>.BadRequest("Invalid login attempt.");
    }

    public async Task<Result<AppUser>> SignUpAsync(SignUpFormData formData)
    {
        if (formData == null)
        {
            return Result<AppUser>.BadRequest("Form data can't null.");
        }

        var result = await _appUserService.AddAppUserUserAsync(formData);

        return result;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
