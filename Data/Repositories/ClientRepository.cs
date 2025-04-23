using Data.Context;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;
public interface IClientRepository : IBaseRepository<ClientEntity, Client>
{
}
public class ClientRepository(AppDbContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
}

public class AppUserRepository(AppDbContext context) : BaseRepository<AppUserEntity, AppUser>(context)
{
}

public class StatusRepository(AppDbContext context) : BaseRepository<StatusEntity, Status>(context)
{
}

public class ProjectRepository(AppDbContext context) : BaseRepository<ProjectEntity, Project>(context)
{
    //UserRepository eller UserManager
}
