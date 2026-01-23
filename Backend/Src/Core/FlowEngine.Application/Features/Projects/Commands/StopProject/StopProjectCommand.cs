using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace FlowEnginex.Application.Features.Projects.Commands.StopProject;

public class StopProjectCommand : IRequest<BaseResult>
{
    public string ProjectName { get; set; }
}