using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.DTOs;
using FlowEngine.Domain.Projects.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEnginex.Application.Features.Projects.Queries.GetUserProjectByName;

public class GetUserProjectByNameQueryHandler(IProjectRepository projectRepository, IAuthenticatedUserService authenticatedUser) : IRequestHandler<GetUserProjectByNameQuery, BaseResult<ProjectDto>>
{
    public async Task<BaseResult<ProjectDto>> Handle(GetUserProjectByNameQuery request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetByNameAsync(new Guid(authenticatedUser.UserId), request.ProjectName);

        return data;
    }
}