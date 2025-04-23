using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public interface IAppUserRepository : IBaseRepository<AppUserEntity, AppUser>
{
}
