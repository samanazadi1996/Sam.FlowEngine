using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;

namespace FlowEngine.Application.Features.Projects.Commands.StopProject;

public class StopProjectCommand : IRequest<BaseResult>
{
    public string ProjectName { get; set; }
}