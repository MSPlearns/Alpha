using Data.Entities;
using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Results;

namespace Business.Services;

public interface IAppUserService
{
    Task<Result<AppUser>> AddUserToRoleAsync(string userId, string roleName);
    Task<Result<IEnumerable<AppUser>>> GetAllAsync();
    Task<Result<AppUser>> GetByIdAsync(string appUserId);
}

public class AppUserService(IAppUserRepository appUserRepository, UserManager<AppUserEntity> userManager, RoleManager<IdentityRole> roleManager) : IAppUserService

{
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly UserManager<AppUserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

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
            return Result<AppUser>.BadRequest(ex.Message);
        }
    }
}
