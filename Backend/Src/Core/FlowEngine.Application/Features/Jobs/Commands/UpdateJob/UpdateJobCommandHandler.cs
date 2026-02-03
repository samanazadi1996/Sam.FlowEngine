using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEnginex.Application.Features.Jobs.Commands.UpdateJob;

public class UpdateJobCommandHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork, IFlowEngineServices flowEngineServices, IAuthenticatedUserService authenticatedUser) : IRequestHandler<UpdateJobCommand, BaseResult>
{
    public async Task<BaseResult> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await jobRepository.GetByIdAsync(request.Id);

        job.Name = request.Name;
        job.JobParameters ??= [];
        job.NextJob = request.NextJob;

        foreach (var item in job.JobParameters)
        {
            if (request.Parameters.TryGetValue(item.Key, out var newValue))
            {
                job.JobParameters[item.Key] = newValue;
            }
            else
            {
                job.JobParameters[item.Key] = null;
            }
        }

        jobRepository.Update(job);
        await unitOfWork.SaveChangesAsync();

        await flowEngineServices.LoadData(job.ProjectId);

        return BaseResult.Ok();
    }
}