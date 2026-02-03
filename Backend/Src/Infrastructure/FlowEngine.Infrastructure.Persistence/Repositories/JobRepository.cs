using FlowEngine.Application.DTOs;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowEngine.Infrastructure.Persistence.Repositories;

public class JobRepository(ApplicationDbContext dbContext) : GenericRepository<ProjectJob>(dbContext), IJobRepository
{
    public async Task<List<IdTitleDto>> GetAllJobsByProjectIdAsync(Guid userId, long projectId)
    {
        var query = dbContext.ProjectJobs
            .Where(p => p.ProjectId == projectId)
            .AsQueryable();


        return await query
            .Select(p => new IdTitleDto { Id = p.Id, Title = p.Name })
            .ToListAsync();

    }
}
