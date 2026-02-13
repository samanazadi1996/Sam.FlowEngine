using FlowEngine.Application.Wrappers;
using FlowEngine.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace FlowEnginex.Application.Features.Jobs.Commands.DeleteJob;

public class DeleteJobCommand : IRequest<BaseResult>
{
    public long Id { get; set; }
}