using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Persistence.Contexts;

namespace FlowEngine.Infrastructure.Persistence.Repositories;

public class JobRepository(ApplicationDbContext dbContext) : GenericRepository<ProjectJob>(dbContext), IJobRepository
{

}
