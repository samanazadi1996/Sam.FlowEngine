using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace FlowEnginex.Application.Features.Jobs.Commands.CreateJob;

public class CreateJobCommand : IRequest<BaseResult>
{
    public long ProjectId { get; set; }
    public string Name { get; set; }
    public string ClassName { get; set; }

}