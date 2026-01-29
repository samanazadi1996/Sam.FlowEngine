using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEnginex.Application.Features.Projects.Queries.GetUserProjectByName;

public class GetUserProjectByNameQueryHandler(IProjectRepository projectRepository, IAuthenticatedUserService authenticatedUser) : IRequestHandler<GetUserProjectByNameQuery, BaseResult<Project>>
{
    public async Task<BaseResult<Project>> Handle(GetUserProjectByNameQuery request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetAllAsync(new Guid(authenticatedUser.UserId), null);

        return data.FirstOrDefault(p => p.ProjectName == request.ProjectName);
    }
}