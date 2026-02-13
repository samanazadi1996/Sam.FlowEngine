using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;

namespace FlowEngine.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<BaseResult<long>>
{
    public string ProjectName { get; set; }
}