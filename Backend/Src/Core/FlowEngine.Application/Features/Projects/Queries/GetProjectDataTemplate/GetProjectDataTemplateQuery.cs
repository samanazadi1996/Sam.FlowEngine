using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace FlowEnginex.Application.Features.Projects.Queries.GetProjectDataTemplate;

public class GetProjectDataTemplateQuery : IRequest<BaseResult<Dictionary<string, string>>>
{
    public long ProjectId { get; set; }
}