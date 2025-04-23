using Data.Context;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public class StatusRepository(AppDbContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{
}
