using Data.Context;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public class AppUserRepository(AppDbContext context) : BaseRepository<AppUserEntity, AppUser>(context), IAppUserRepository
{
}
