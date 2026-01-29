using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Projects.Entities;

namespace FlowEnginex.Application.Features.Projects.Queries.GetUserProjectByName;

public class GetUserProjectByNameQuery : IRequest<BaseResult<Project>>
{
    public string ProjectName { get; set; }
}