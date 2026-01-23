using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.WebApi.Infrastructure.Extensions;
using FlowEnginex.Application.Features.Projects.Commands.StartProject;
using FlowEnginex.Application.Features.Projects.Commands.StopProject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace FlowEngine.WebApi.Endpoints
{
    public class ProjectEndpoint : EndpointGroupBase
    {
        public override void Map(RouteGroupBuilder builder)
        {

            builder.MapPost(CteateTemplate).RequireAuthorization();
            builder.MapPost(StartProject).RequireAuthorization();
            builder.MapPost(StopProject).RequireAuthorization();
        }

        async Task<BaseResult> CteateTemplate(IFlowEngineServices flowEngine, string templateName)
        {
            await flowEngine.CteateTemplate(templateName);

            return BaseResult.Ok();
        }

        async Task<BaseResult> StartProject(IMediator mediator, StartProjectCommand model)
            => await mediator.Send<StartProjectCommand, BaseResult>(model);

        async Task<BaseResult> StopProject(IMediator mediator, StopProjectCommand model)
            => await mediator.Send<StopProjectCommand, BaseResult>(model);
    }
}
