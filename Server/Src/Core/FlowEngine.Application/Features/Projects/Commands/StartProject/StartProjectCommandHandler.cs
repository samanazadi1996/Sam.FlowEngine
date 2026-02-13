using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Projects.Commands.StartProject;

public class StartProjectCommandHandler(IFlowEngineServices flowEngine, IAuthenticatedUserService authenticatedUser, IUnitOfWork unitOfWork, IProjectRepository projectRepository) : IRequestHandler<StartProjectCommand, BaseResult>
{
    public async Task<BaseResult> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetByNameAsync(new Guid(authenticatedUser.UserId), request.ProjectName);
        if (data is null)
            return new Error(ErrorCode.NotFound);

        data.Started = true;

        await unitOfWork.SaveChangesAsync();

        await flowEngine.Start(data.Id);

        return BaseResult.Ok();
    }
}