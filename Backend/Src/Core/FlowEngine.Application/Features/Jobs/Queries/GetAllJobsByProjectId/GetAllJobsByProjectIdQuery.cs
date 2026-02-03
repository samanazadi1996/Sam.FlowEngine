using FlowEngine.Application.DTOs;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using System.Collections.Generic;

namespace FlowEnginex.Application.Features.Jobs.Queries.GetAllJobsByProjectId;

public class GetAllJobsByProjectIdQuery : IRequest<BaseResult<List<IdTitleDto>>>
{
    public long ProjectId { get; set; }
}