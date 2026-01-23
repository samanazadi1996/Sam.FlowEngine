using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;

namespace FlowEnginex.Application.Features.Projects.Commands.StopProject;

public class StopProjectCommand : IRequest<BaseResult>
{
    public string ProjectName { get; set; }
}