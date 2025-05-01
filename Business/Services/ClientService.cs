using Data.Entities;
using Data.Repositories;
using Domain.Dtos;
using Domain.Models;
using Shared.Extensions;
using Shared.Results;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService

{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<Result<Client>> AddAsync(AddClientFormData formData)
    {
        await _clientRepository.BeginTransactionAsync();
        try
        {
            // TODO: Send DTO to factory
            // TODO: Factory returns entity
            var client = formData.MapTo<ClientEntity>(); //Remove when DTO and factory are implemented

            await _clientRepository.AddAsync(client);
            await _clientRepository.SaveAsync();

            await _clientRepository.CommitTransactionAsync();
            return Result<Client>.Created();
        }
        catch (Exception ex)
        {
            await _clientRepository.RollbackTransactionAsync();
            return Result<Client>.BadRequest(ex.Message);
        }
    }


    public async Task<Result<IEnumerable<Client>>> GetAllAsync()
    {
        try
        {
            var result = await _clientRepository.GetAllAsync();
            return result;
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Client>>.BadRequest(ex.Message);
        }
    }

    public async Task<Result<Client>> GetByIdAsync(string clientId)
    {
        try
        {
            var result = await _clientRepository.GetAsync(x => x.Id == clientId);
            return result;
        }
        catch (Exception ex)
        {
            return Result<Client>.BadRequest(ex.Message);
        }
    }


    public async Task<Result<Client>> UpdateAsync(EditClientFormData form, Client existingClient)
    {
        await _clientRepository.BeginTransactionAsync();
        try
        {
            // TODO: Send DTO to factory
            // TODO: Factory returns entity
            var updatedClient = form.MapTo<ClientEntity>(); //Remove when DTO and factory are implemented

            var result = await _clientRepository.UpdateAsync(x => x.Id == existingClient.Id, updatedClient);
            await _clientRepository.SaveAsync();

            await _clientRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _clientRepository.RollbackTransactionAsync();
            return Result<Client>.BadRequest(ex.Message);
        }
    }


    public async Task<Result<Client>> DeleteAsync(Client client)
    {
        await _clientRepository.BeginTransactionAsync();
        try
        {
            if (client == null)
            {
                await _clientRepository.RollbackTransactionAsync();
                return Result<Client>.BadRequest("Client is null");
            }
            var exists = await _clientRepository.EntityExistsAsync(x => x.Id == client.Id);
            if (!exists.Succeeded)
            {
                await _clientRepository.RollbackTransactionAsync();
                return Result<Client>.NotFound("Client not found");
            }

            var result = _clientRepository.Delete(client);
            await _clientRepository.SaveAsync();
            await _clientRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _clientRepository.RollbackTransactionAsync();
            return Result<Client>.BadRequest(ex.Message);
        }
    }
}
