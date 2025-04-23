using Data.Context;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;
public class ClientRepository(AppDbContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
}
