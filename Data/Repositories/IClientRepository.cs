using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{
}
