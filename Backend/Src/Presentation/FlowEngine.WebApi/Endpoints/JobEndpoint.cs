using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace FlowEngine.WebApi.Endpoints;

public class JobEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {

        builder.MapGet(GetAllJobs).RequireAuthorization();
    }


    BaseResult<List<ProjectJob>> GetAllJobs(IFlowEngineServices flowEngine)
        => flowEngine.GetAllJobs();

}
