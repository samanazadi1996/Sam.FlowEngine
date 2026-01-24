using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Projects.Queries.GetUserProjects;

public class GetUserProjectsQueryHandler(IProjectRepository projectRepository,IAuthenticatedUserService authenticatedUser) : IRequestHandler<GetUserProjectsQuery, BaseResult<List<GetUserProjectsResponse>>>
{
    public async Task<BaseResult<List<GetUserProjectsResponse>>> Handle(GetUserProjectsQuery request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetAllAsync(new Guid(authenticatedUser.UserId), null);

        return data.Select(x => new GetUserProjectsResponse() {
            ProjectName = x.ProjectName,
            Started=x.Started
        }).ToList();
    }
}