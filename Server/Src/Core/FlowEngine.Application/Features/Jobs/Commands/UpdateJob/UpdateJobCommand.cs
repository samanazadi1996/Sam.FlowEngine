using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace FlowEnginex.Application.Features.Jobs.Commands.UpdateJob;

public class UpdateJobCommand : IRequest<BaseResult>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string,string> Parameters { get; set; }
    public List<long> NextJob { get; set; }
}