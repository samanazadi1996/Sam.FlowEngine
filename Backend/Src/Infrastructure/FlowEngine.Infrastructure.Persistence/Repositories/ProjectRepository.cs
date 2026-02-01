using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Domain.Projects.DTOs;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowEngine.Infrastructure.Persistence.Repositories;
public class ProjectRepository(ApplicationDbContext dbContext) : GenericRepository<Project>(dbContext), IProjectRepository
{
    //public async Task<PaginationResponseDto<ProjectDto>> GetPagedListAsync(int pageNumber, int pageSize, string name)
    //{

    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        query = query.Where(p => p.Name.Contains(name));
    //    }

    //    return await Paged(
    //        query.Select(p => new ProjectDto(p)),
    //        pageNumber,
    //        pageSize);

    //}
    public async Task<List<Project>> GetAllAsync(Guid? userId, long? projectId)
    {
        var query = dbContext.Projects
            .Include(p => p.ProjectJobs)
            .AsQueryable();

        if (userId is not null)
            query = query.Where(p => p.CreatedBy == userId);


        if (projectId.HasValue && projectId != 0)
            query = query.Where(p => p.Id == projectId);

        return await query.ToListAsync();

    }

    public async Task<Project> GetByNameAsync(Guid userId, string projectName)
    {
        return await dbContext.Projects
            .Where(p => p.ProjectName == projectName)
            .Where(p => p.CreatedBy == userId)
            .Include(p => p.ProjectJobs)
            .FirstOrDefaultAsync();

    }
}
