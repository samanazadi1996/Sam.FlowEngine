using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;

namespace FlowEngine.Application.Features.Projects.Commands.StartProject;

public class StartProjectCommand : IRequest<BaseResult>
{
    public string ProjectName { get; set; }
}