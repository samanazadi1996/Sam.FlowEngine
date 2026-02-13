using FlowEngine.Application.DTOs.Account.Requests;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;

namespace FlowEngine.Application.Features.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommand : ChangePasswordRequest, IRequest<BaseResult>
    {
    }
}