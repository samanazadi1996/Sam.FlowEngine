using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;
using FlowEngine.Domain.Projects.DTOs;

namespace FlowEnginex.Application.Features.Jobs.Queries.GetJobById;

public class GetJobByIdQuery : IRequest<BaseResult<ProjectJobDto>>
{
    public long Id { get; set; }
}