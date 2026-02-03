using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using FlowEngine.Application.DTOs;
using FlowEngine.Application.Interfaces.Repositories;

namespace FlowEnginex.Application.Features.Jobs.Queries.GetAllJobsByProjectId;

public class GetAllJobsByProjectIdQueryHandler(IJobRepository jobRepository, IAuthenticatedUserService authenticatedUser) : IRequestHandler<GetAllJobsByProjectIdQuery, BaseResult<List<IdTitleDto>>>
{
    public async Task<BaseResult<List<IdTitleDto>>> Handle(GetAllJobsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        return await jobRepository.GetAllJobsByProjectIdAsync(new Guid(authenticatedUser.UserId), request.ProjectId);
    }
}