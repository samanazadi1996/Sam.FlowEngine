using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.WebApi.Infrastructure.Extensions;
using FlowEnginex.Application.Features.Jobs.Commands.UpdatePositionJob;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEngine.WebApi.Endpoints;

public class JobEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {

        builder.MapGet(GetAllJobs).RequireAuthorization();

        builder.MapPut(UpdatePositionJob).RequireAuthorization();
    }


    BaseResult<List<ProjectJob>> GetAllJobs(IFlowEngineServices flowEngine)
        => flowEngine.GetAllJobs();


    async Task<BaseResult> UpdatePositionJob(IMediator mediator, UpdatePositionJobCommand model)
        => await mediator.Send<UpdatePositionJobCommand, BaseResult>(model);


}
