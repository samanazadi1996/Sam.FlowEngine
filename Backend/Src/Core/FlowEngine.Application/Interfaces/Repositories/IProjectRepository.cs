using FlowEngine.Domain.Projects.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        //Task<PaginationResponseDto<ProjectDto>> GetPagedListAsync(int pageNumber, int pageSize, string name);
        Task<List<Project>> GetAllAsync(Guid userId, string projectName);
    }
}
