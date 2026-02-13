using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace FlowEnginex.Application.Features.Jobs.Commands.UpdatePositionJob;

public class UpdatePositionJobCommand : IRequest<BaseResult>
{
    public long JobId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}