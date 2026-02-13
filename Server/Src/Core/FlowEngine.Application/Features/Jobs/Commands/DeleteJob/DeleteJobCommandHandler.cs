using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEnginex.Application.Features.Jobs.Commands.DeleteJob;

public class DeleteJobCommandHandler(
    IProjectRepository projectRepository,
    IFlowEngineServices flowEngineServices,
    IJobRepository jobRepository,
    IAuthenticatedUserService authenticatedUser,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteJobCommand, BaseResult>
{
    public async Task<BaseResult> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var job = await jobRepository.GetByIdAsync(request.Id);
        if (job is null)
            return new Error(ErrorCode.NotFound);

        var project = await projectRepository.GetProjectWithJobsByIdAsync(new Guid(authenticatedUser.UserId), job.ProjectId);
        if (project is null)
            return new Error(ErrorCode.NotFound);

        foreach (var item in project.ProjectJobs.Where(p => p.NextJob is not null && p.NextJob.Any(x => x == job.Id)))
        {
            item.NextJob = [.. item.NextJob.Where(p => p != job.Id)];
            jobRepository.Update(item);
        }

        jobRepository.Delete(job);
        
        await unitOfWork.SaveChangesAsync();

        await flowEngineServices.LoadData(job.ProjectId);

        return BaseResult.Ok();
    }
}