using FlowEngine.Application.DTOs;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.DTOs;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.WebApi.Infrastructure.Extensions;
using FlowEnginex.Application.Features.Jobs.Commands.UpdateJob;
using FlowEnginex.Application.Features.Jobs.Commands.UpdatePositionJob;
using FlowEnginex.Application.Features.Jobs.Queries.GetAllJobsByProjectId;
using FlowEnginex.Application.Features.Jobs.Queries.GetJobById;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEngine.WebApi.Endpoints;

public class JobEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {

        builder.MapGet(GetAllJobsByProjectId).RequireAuthorization();

        builder.MapGet(GetJobById).RequireAuthorization();

        builder.MapPut(UpdatePositionJob).RequireAuthorization();

        builder.MapPut(UpdateJob).RequireAuthorization();
    }


    async Task<BaseResult<List<IdTitleDto>>> GetAllJobsByProjectId(IMediator mediator, [AsParameters] GetAllJobsByProjectIdQuery model)
         => await mediator.Send<GetAllJobsByProjectIdQuery, BaseResult<List<IdTitleDto>>>(model);

    async Task<BaseResult<ProjectJobDto>> GetJobById(IMediator mediator, [AsParameters] GetJobByIdQuery model)
        => await mediator.Send<GetJobByIdQuery, BaseResult<ProjectJobDto>>(model);

    async Task<BaseResult> UpdatePositionJob(IMediator mediator, UpdatePositionJobCommand model)
        => await mediator.Send<UpdatePositionJobCommand, BaseResult>(model);

    async Task<BaseResult> UpdateJob(IMediator mediator, UpdateJobCommand model)
        => await mediator.Send<UpdateJobCommand, BaseResult>(model);


}
