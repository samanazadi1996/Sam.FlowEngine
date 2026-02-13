using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Projects.Commands.StopProject;

public class StopProjectCommandHandler(IFlowEngineServices flowEngine, IAuthenticatedUserService authenticatedUser, IUnitOfWork unitOfWork, IProjectRepository projectRepository) : IRequestHandler<StopProjectCommand, BaseResult>
{
    public async Task<BaseResult> Handle(StopProjectCommand request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetByNameAsync(new Guid(authenticatedUser.UserId), request.ProjectName);
        if (data is null)
            return new Error(ErrorCode.NotFound);

        data.Started = false;

        await unitOfWork.SaveChangesAsync();

        await flowEngine.Stop(data.Id);

        return BaseResult.Ok();
    }
}