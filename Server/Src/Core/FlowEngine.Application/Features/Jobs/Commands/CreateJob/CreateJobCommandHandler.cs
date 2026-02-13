using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Domain.Projects.Entities;
using System.Linq;

namespace FlowEnginex.Application.Features.Jobs.Commands.CreateJob;

public class CreateJobCommandHandler(IFlowEngineServices flowEngine, IUnitOfWork unitOfWork, IJobRepository jobRepository) : IRequestHandler<CreateJobCommand, BaseResult>
{
    public async Task<BaseResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var tempJob = flowEngine.GetAllJobs().FirstOrDefault(p => p.ClassName == request.ClassName);

        if (tempJob == null)
            return new Error(ErrorCode.NotFound, request.ClassName);

        var job = new ProjectJob()
        {
            Name = request.Name,
            ClassName = request.ClassName,
            JobParameters = tempJob.JobParameters,
            ProjectId = request.ProjectId,
            Position = new(50, 50)
        };

        await jobRepository.AddAsync(job);

        await unitOfWork.SaveChangesAsync();

        await flowEngine.LoadData(job.ProjectId);

        return BaseResult.Ok();
    }
}