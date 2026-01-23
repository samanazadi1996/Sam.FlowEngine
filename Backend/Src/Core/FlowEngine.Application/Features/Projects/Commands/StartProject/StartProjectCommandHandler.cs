using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace FlowEnginex.Application.Features.Projects.Commands.StartProject;

public class StartProjectCommandHandler(IFlowEngineServices flowEngine, IAuthenticatedUserService authenticatedUser, IUnitOfWork unitOfWork, IProjectRepository projectRepository) : IRequestHandler<StartProjectCommand, BaseResult>
{
    public async Task<BaseResult> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetAllAsync(new Guid(authenticatedUser.UserId), request.ProjectName);
        if (data.Count == 0)
            return new Error(ErrorCode.NotFound);

        foreach (var item in data)
            item.Started = true;

        await unitOfWork.SaveChangesAsync();

        await flowEngine.Start(request.ProjectName);

        return BaseResult.Ok();
    }
}