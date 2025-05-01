using Data.Entities;
using Data.Repositories;
using Domain.Dtos;
using Domain.Models;
using Shared.Extensions;
using Shared.Results;

namespace Business.Services;

public interface IProjectService
{
    Task<Result<Project>> AddAsync(AddProjectFormData formData);
    Task<Result<Project>> DeleteAsync(Project project);
    Task<Result<IEnumerable<Project>>> GetAllAsync();
    Task<Result<Project>> GetById(int projectId);
    Task<Result<Project>> UpdateAsync(EditProjectFormData formData, string existingProjectId);
}

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService, IClientService clientService, IAppUserService appUserService) : IProjectService

{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;
    private readonly IClientService _clientService = clientService;
    private readonly IAppUserService _appUserService = appUserService;
    public async Task<Result<Project>> AddAsync(AddProjectFormData formData)
    {
        if (formData == null)
        {
            return Result<Project>.BadRequest("Form data is invalid.");
        }
        await _projectRepository.BeginTransactionAsync();
        try
        {
            // TODO: Send DTO to factory
            // TODO: Factory returns entity
            var project = formData.MapTo<ProjectEntity>(); //Remove when DTO and factory are implemented. I want more control over the mapping process
            var status = await _statusService.GetByIdAsync(formData.StatusId);
            if (status == null)
            {
                await _projectRepository.RollbackTransactionAsync();
                return Result<Project>.BadRequest("Status not found.");
            }
            var client = await _clientService.GetByIdAsync(formData.ClientId);
            if (client == null)
            {
                await _projectRepository.RollbackTransactionAsync();
                return Result<Project>.BadRequest("Client not found.");
            }
            var projectManager = await _appUserService.GetByIdAsync(formData.ProjectManagerId);
            if (projectManager == null)
            {
                await _projectRepository.RollbackTransactionAsync();
                return Result<Project>.BadRequest("Project manager - user not found.");
            }

            project.ProjectStatus = status.Data!.MapTo<StatusEntity>();
            project.ProjectClient = client.Data!.MapTo<ClientEntity>();
            project.ProjectManager = projectManager.Data!.MapTo<AppUserEntity>();


            var result = await _projectRepository.AddAsync(project);
            await _projectRepository.SaveAsync();

            await _projectRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            return Result<Project>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Project>>> GetAllAsync()
    {
        try
        {
            var result = await _projectRepository.GetAllAsync
            (
                orderByDescending: true,
                sortBy: s => s.CreatedDate,
                where: null,
                include => include.ProjectClient,
                include => include.ProjectManager,
                include => include.ProjectStatus
            );
            return result;
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Project>>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<Project>> GetById(int projectId)
    {
        try
        {
            var result = await _projectRepository.GetAsync
            (
                x => x.Id == projectId.ToString(),
                include => include.ProjectClient,
                include => include.ProjectManager,
                include => include.ProjectStatus
            );
            return result;
        }
        catch (Exception ex)
        {
            return Result<Project>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<Project>> UpdateAsync(EditProjectFormData formData, string existingProjectId)
    {
        if (formData == null)
        {
            return Result<Project>.BadRequest("Form data is invalid.");
        }
        await _projectRepository.BeginTransactionAsync();
        try
        {
            // TODO: Send DTO to factory
            // TODO: Factory returns entity
            var updatedProject = formData.MapTo<ProjectEntity>(); //Remove when DTO and factory are implemented. I want more control over the mapping process
           
            var status = await _statusService.GetByIdAsync(formData.StatusId);
            if (status == null)
            {
                await _projectRepository.RollbackTransactionAsync();
                return Result<Project>.BadRequest("Status not found.");
            }
            var client = await _clientService.GetByIdAsync(formData.ClientId);
            if (client == null)
            {
                await _projectRepository.RollbackTransactionAsync();
                return Result<Project>.BadRequest("Client not found.");
            }
            var projectManager = await _appUserService.GetByIdAsync(formData.ProjectManagerId);
            if (projectManager == null)
            {
                await _projectRepository.RollbackTransactionAsync();
                return Result<Project>.BadRequest("Project manager - user not found.");
            }

            updatedProject.ProjectStatus = status.Data!.MapTo<StatusEntity>();
            updatedProject.ProjectClient = client.Data!.MapTo<ClientEntity>();
            updatedProject.ProjectManager = projectManager.Data!.MapTo<AppUserEntity>();

            var result = await _projectRepository.UpdateAsync(x => x.Id == existingProjectId, updatedProject);
            await _projectRepository.SaveAsync();

            await _projectRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            return Result<Project>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<Project>> DeleteAsync(Project project)
    {
        await _projectRepository.BeginTransactionAsync();
        try
        {
            var projectExists = await _projectRepository.EntityExistsAsync(x => x.Id == project.Id);
            if (project == null || !projectExists.Succeeded)
            {
                await _projectRepository.RollbackTransactionAsync();
                return Result<Project>.BadRequest("Project is null");
            }

            var result = _projectRepository.Delete(project!);
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            return Result<Project>.BadRequest(ex.Message);
        }
    }
}