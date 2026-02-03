using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace FlowEnginex.Application.Features.Projects.Queries.GetProjectDataTemplate;

public class GetProjectDataTemplateQueryHandler(IFlowEngineServices flowEngineServices) : IRequestHandler<GetProjectDataTemplateQuery, BaseResult<Dictionary<string, string>>>
{
    public async Task<BaseResult<Dictionary<string, string>>> Handle(GetProjectDataTemplateQuery request, CancellationToken cancellationToken)
    {
        var ddd = flowEngineServices.GetData(request.ProjectId) ?? [];

        return ddd;
    }
}