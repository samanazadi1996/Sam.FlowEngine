using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowEngine.Infrastructure.Persistence.Repositories
{
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
        public async Task<List<Project>> GetAllAsync(Guid userId, string projectName)
        {
            var query = dbContext.Projects.AsQueryable();


            if (projectName != "*")
            {
                query = query
                    .Where(p => p.ProjectName == projectName)
                    .Where(p => p.CreatedBy == userId);

            }

            return await query.ToListAsync();

        }
    }
}
