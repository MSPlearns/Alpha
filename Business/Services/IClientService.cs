using Business.Models.Dtos;

namespace Business.Services;

public interface IClientService
{
    Task<bool> CreateClientAsync(AddClientForm form);
    Task<bool> UpdateClientAsync(EditClientForm form);
    bool DeleteClient(int clientId);
    //Task<List<Client>> GetClients();
    //Task<Client> GetClient(int clientId);
}
