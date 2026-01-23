using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEnginex.Application.Features.Projects.Commands.StopProject;

public class StopProjectCommandHandler(IFlowEngineServices flowEngine, IAuthenticatedUserService authenticatedUser, IUnitOfWork unitOfWork, IProjectRepository projectRepository) : IRequestHandler<StopProjectCommand, BaseResult>
{
    public async Task<BaseResult> Handle(StopProjectCommand request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetAllAsync(new Guid(authenticatedUser.UserId), request.ProjectName);
        if (data.Count == 0)
            return new Error(ErrorCode.NotFound);

        foreach (var item in data)
            item.Started = false;

        await unitOfWork.SaveChangesAsync();

        await flowEngine.Stop(request.ProjectName);

        return BaseResult.Ok();
    }
}