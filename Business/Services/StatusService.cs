using Data.Entities;
using Data.Repositories;
using Domain.Dtos;
using Domain.Models;
using Shared.Extensions;
using Shared.Results;

namespace Business.Services;

public interface IStatusService
{
    Task<Result<Status>> AddAsync(AddStatusFormData formData);
    Task<Result<Status>> DeleteAsync(Status status);
    Task<Result<IEnumerable<Status>>> GetAllAsync();
    Task<Result<Status>> GetByIdAsync(int statusId);
    Task<Result<Status>> GetByName(string statusName);
    Task<Result<Status>> UpdateAsync(Status existingStatus);
}

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<Result<Status>> AddAsync(AddStatusFormData formData)
    {
        await _statusRepository.BeginTransactionAsync();
        try
        {
            // TODO: Send DTO to factory
            // TODO: Factory returns entity
            var status = formData.MapTo<StatusEntity>(); //Remove when DTO and factory are implemented

            await _statusRepository.AddAsync(status);
            await _statusRepository.SaveAsync();

            await _statusRepository.CommitTransactionAsync();
            return Result<Status>.Created();
        }
        catch (Exception ex)
        {
            await _statusRepository.RollbackTransactionAsync();
            return Result<Status>.BadRequest(ex.Message);
        }
    }


    public async Task<Result<IEnumerable<Status>>> GetAllAsync()
    {
        try
        {
            var result = await _statusRepository.GetAllAsync();
            return result;
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Status>>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<Status>> GetByIdAsync(int statusId)
    {
        try
        {
            var result = await _statusRepository.GetAsync(x => x.Id == statusId);
            return result;
        }
        catch (Exception ex)
        {
            return Result<Status>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<Status>> GetByName(string statusName)
    {
        try
        {
            var result = await _statusRepository.GetAsync(x => x.Name == statusName);
            return result;
        }
        catch (Exception ex)
        {
            return Result<Status>.BadRequest(ex.Message);
        }
    }


    public async Task<Result<Status>> UpdateAsync(Status existingStatus)
    {
        await _statusRepository.BeginTransactionAsync();
        try
        {
            // TODO: Send DTO to factory
            // TODO: Factory returns entity
            var updatedStatus = new StatusEntity(); //Remove when DTO and factory are implemented

            await _statusRepository.UpdateAsync(x => x.Id == existingStatus.Id, updatedStatus);
            await _statusRepository.SaveAsync();

            await _statusRepository.CommitTransactionAsync();
            return Result<Status>.Updated();
        }
        catch (Exception ex)
        {
            await _statusRepository.RollbackTransactionAsync();
            return Result<Status>.BadRequest(ex.Message);
        }
    }


    public async Task<Result<Status>> DeleteAsync(Status status)
    {
        await _statusRepository.BeginTransactionAsync();
        try
        {
            if (status == null)
            {
                await _statusRepository.RollbackTransactionAsync();
                return Result<Status>.BadRequest("Status is null");
            }
            var exists = await _statusRepository.GetAsync(x => x.Id == status.Id);
            if (!exists.Succeeded)
            {
                await _statusRepository.RollbackTransactionAsync();
                return Result<Status>.NotFound("Status does not exist");
            }

            _statusRepository.Delete(status);
            await _statusRepository.SaveAsync();
            await _statusRepository.CommitTransactionAsync();
            return Result<Status>.Ok();
        }
        catch (Exception ex)
        {
            await _statusRepository.RollbackTransactionAsync();
            return Result<Status>.BadRequest(ex.Message);
        }
    }
}
