using Data.Context;
using Data.Entities;
using Domain.Models;

namespace Data.Repositories;

public class ProjectRepository(AppDbContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
    //UserRepository eller UserManager
}
