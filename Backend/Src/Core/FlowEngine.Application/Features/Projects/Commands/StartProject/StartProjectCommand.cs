using System;
using System.Collections.Generic;
using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;

namespace FlowEnginex.Application.Features.Projects.Commands.StartProject;

public class StartProjectCommand : IRequest<BaseResult>
{
    public string ProjectName { get; set; }
}