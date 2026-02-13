using FlowEngine.Application.Features.Projects.Commands.CreateProject;
using FlowEngine.Application.Features.Projects.Commands.StartProject;
using FlowEngine.Application.Features.Projects.Commands.StopProject;
using FlowEngine.Application.Features.Projects.Queries.GetUserProjects;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.DTOs;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Helpers;
using FlowEngine.WebApi.Infrastructure.Extensions;
using FlowEnginex.Application.Features.Projects.Queries.GetProjectDataTemplate;
using FlowEnginex.Application.Features.Projects.Queries.GetUserProjectByName;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEngine.WebApi.Endpoints;

public class ProjectEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {

        builder.MapGet(GetUserProjects).RequireAuthorization();
        builder.MapGet(GetUserProjectByName).RequireAuthorization();

        builder.MapPost(StartProject).RequireAuthorization();
        builder.MapPost(StopProject).RequireAuthorization();
        builder.MapPost(CreateProject).RequireAuthorization();

        builder.MapPost(CteateTemplate).RequireAuthorization();

        builder.MapGet(GetProjectDataTemplate).RequireAuthorization();
    }


    async Task<BaseResult<List<GetUserProjectsResponse>>> GetUserProjects(IMediator mediator, [AsParameters] GetUserProjectsQuery model)
        => await mediator.Send<GetUserProjectsQuery, BaseResult<List<GetUserProjectsResponse>>>(model);
    async Task<BaseResult<ProjectDto>> GetUserProjectByName(IMediator mediator, [AsParameters] GetUserProjectByNameQuery model)
        => await mediator.Send<GetUserProjectByNameQuery, BaseResult<ProjectDto>>(model);

    async Task<BaseResult> CreateProject(IMediator mediator, CreateProjectCommand model)
        => await mediator.Send<CreateProjectCommand, BaseResult<long>>(model);

    async Task<BaseResult> StartProject(IMediator mediator, StartProjectCommand model)
        => await mediator.Send<StartProjectCommand, BaseResult>(model);

    async Task<BaseResult> StopProject(IMediator mediator, StopProjectCommand model)
        => await mediator.Send<StopProjectCommand, BaseResult>(model);
    async Task<BaseResult<Dictionary<string, string>>> GetProjectDataTemplate(IMediator mediator, [AsParameters] GetProjectDataTemplateQuery model)
        => await mediator.Send<GetProjectDataTemplateQuery, BaseResult<Dictionary<string, string>>>(model);

    async Task<BaseResult> CteateTemplate(IFlowEngineServices flowEngine, string templateName)
    {
        await flowEngine.CteateTemplate(templateName);

        return BaseResult.Ok();
    }
}
