using FlowEngine.Application.DTOs;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces.Repositories;

public interface IJobRepository : IGenericRepository<ProjectJob>
{
    Task<List<IdTitleDto>> GetAllJobsByProjectIdAsync(Guid guid, long projectId);
}
