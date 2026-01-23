using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;

namespace FlowEngine.Application.Features.Accounts.Commands.Start
{
    public class StartCommand : IRequest<BaseResult<AuthenticationResponse>>
    {
    }
}