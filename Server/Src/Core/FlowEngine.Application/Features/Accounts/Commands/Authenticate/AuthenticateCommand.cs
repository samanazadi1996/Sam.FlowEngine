using FlowEngine.Application.DTOs.Account.Requests;
using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;

namespace FlowEngine.Application.Features.Accounts.Commands.Authenticate
{
    public class AuthenticateCommand : AuthenticationRequest, IRequest<BaseResult<AuthenticationResponse>>
    {
    }
}