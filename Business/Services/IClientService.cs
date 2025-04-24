using Domain.Dtos;
using Domain.Models;
using Shared.Results;

namespace Business.Services
{
    public interface IClientService
    {
        Task<Result<Client>> AddAsync(AddClientFormData formData);
        Task<Result<Client>> DeleteAsync(Client client);
        Task<Result<IEnumerable<Client>>> GetAllAsync();
        Task<Result<Client>> GetByIdAsync(string clientId);
        Task<Result<Client>> UpdateAsync(EditClientFormData form, Client existingClient);
    }
}