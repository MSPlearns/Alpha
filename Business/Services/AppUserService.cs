using Data.Entities;
using Data.Repositories;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;
using Shared.Results;
using System.Diagnostics;

namespace Business.Services;

public interface IAppUserService
{
    Task<Result<AppUser>> AddUserToRoleAsync(string userId, string roleName);
    Task<Result<IEnumerable<AppUser>>> GetAllAsync();
    Task<Result<AppUser>> GetByIdAsync(string appUserId);
    Task<Result<AppUser>> AddAppUserUserAsync(SignUpFormData formData, string roleName = "User");
}

public class AppUserService(IAppUserRepository appUserRepository, UserManager<AppUserEntity> userManager, RoleManager<IdentityRole> roleManager) : IAppUserService

{
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly UserManager<AppUserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<Result<AppUser>> AddAppUserUserAsync(SignUpFormData formData, string roleName = "User")
    {
        if (formData == null)
        {
            return Result<AppUser>.BadRequest("Form data can't null.");
        }

        var existsResult = await _appUserRepository.EntityExistsAsync(x => x.Email == formData.Email);
        if (existsResult.Succeeded == true )
        {
            return Result<AppUser>.AlreadyExists("User with this email already exists");
        }

        try
        {
            var appUserEntity = formData.MapTo<AppUserEntity>();
            var result = _userManager.CreateAsync(appUserEntity, formData.Password);

            if (result.Result.Succeeded)
            {
                var addToRoleResult = await AddUserToRoleAsync(appUserEntity.Id, roleName); 
                return addToRoleResult.Succeeded
                    ? Result<AppUser>.Ok()
                    : Result<AppUser>.PartialSuccess($"User created but not added to default role due to error:{addToRoleResult.ErrorMessage}");
            }
            
            return Result<AppUser>.BadRequest($"Unable to create user due to error: {result.Result.Errors.FirstOrDefault()?.Description}");

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result<AppUser>.BadRequest(ex.Message);
        }
    }



    public async Task<Result<AppUser>> AddUserToRoleAsync(string userId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            return Result<AppUser>.NotFound($"Role {roleName} does not exist");
        }
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result<AppUser>.NotFound($"User with ID {userId} does not exist");
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded
                ? Result<AppUser>.Ok()
                : Result<AppUser>.InternalError(result.Errors.FirstOrDefault()?.Description);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result<AppUser>.BadRequest(ex.Message);
        }
    }
    public async Task<Result<IEnumerable<AppUser>>> GetAllAsync()
    {
        try
        {
            var result = await _appUserRepository.GetAllAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result<IEnumerable<AppUser>>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<AppUser>> GetByIdAsync(string appUserId)
    {
        try
        {
            var result = await _appUserRepository.GetAsync(x => x.Id == appUserId);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result<AppUser>.BadRequest(ex.Message);
        }
    }
}
