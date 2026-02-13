using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using System.Collections.Generic;

namespace FlowEngine.Application.Features.Projects.Queries.GetUserProjects;

public class GetUserProjectsQuery : IRequest<BaseResult<List<GetUserProjectsResponse>>>
{
}