using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.Repositories;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEnginex.Application.Features.Jobs.Queries.GetJobById;

public class GetJobByIdQueryHandler(IJobRepository jobRepository) : IRequestHandler<GetJobByIdQuery, BaseResult<ProjectJobDto>>
{
    public async Task<BaseResult<ProjectJobDto>> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await jobRepository.GetByIdAsync(request.Id);


        return new ProjectJobDto
        {
            Id=job.Id,
            ClassName=job.ClassName,
            JobParameters=job.JobParameters,
            Name = job.Name,
            NextJob = job.NextJob,
            Position = job.Position,           

        };
    }
}