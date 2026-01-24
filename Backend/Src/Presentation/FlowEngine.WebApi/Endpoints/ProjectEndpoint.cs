using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.WebApi.Infrastructure.Extensions;
using FlowEngine.Application.Features.Projects.Commands.StartProject;
using FlowEngine.Application.Features.Projects.Commands.StopProject;
using FlowEngine.Application.Features.Projects.Queries.GetUserProjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlowEngine.WebApi.Endpoints;

public class ProjectEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {

        builder.MapGet(GetUserProjects).RequireAuthorization();

        builder.MapPost(CteateTemplate).RequireAuthorization();
        builder.MapPost(StartProject).RequireAuthorization();
        builder.MapPost(StopProject).RequireAuthorization();
    }


    async Task<BaseResult<List<GetUserProjectsResponse>>> GetUserProjects(IMediator mediator,[AsParameters] GetUserProjectsQuery model)
        => await mediator.Send<GetUserProjectsQuery, BaseResult<List<GetUserProjectsResponse>>>(model);

    async Task<BaseResult> StartProject(IMediator mediator, StartProjectCommand model)
        => await mediator.Send<StartProjectCommand, BaseResult>(model);

    async Task<BaseResult> StopProject(IMediator mediator, StopProjectCommand model)
        => await mediator.Send<StopProjectCommand, BaseResult>(model);

    async Task<BaseResult> CteateTemplate(IFlowEngineServices flowEngine, string templateName)
    {
        await flowEngine.CteateTemplate(templateName);

        return BaseResult.Ok();
    }
}
