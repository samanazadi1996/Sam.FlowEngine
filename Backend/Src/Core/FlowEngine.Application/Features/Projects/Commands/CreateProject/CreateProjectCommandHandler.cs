using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<CreateProjectCommand, BaseResult<long>>
{
    public async Task<BaseResult<long>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var data = await projectRepository.GetByNameAsync(new Guid(authenticatedUser.UserId), request.ProjectName);
        if (data is not null)
            return new Error(ErrorCode.DuplicateData);

        var project = new Project(request.ProjectName)
        {
            Started = false
        };

        await projectRepository.AddAsync(project);

        await unitOfWork.SaveChangesAsync();

        return project.Id;
    }
}