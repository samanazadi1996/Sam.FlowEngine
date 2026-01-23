using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.UserInterfaces;
using FlowEngine.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Accounts.Commands.Start
{
    public class StartCommandHandler(IAccountServices accountServices) : IRequestHandler<StartCommand, BaseResult<AuthenticationResponse>>
    {
        public async Task<BaseResult<AuthenticationResponse>> Handle(StartCommand request, CancellationToken cancellationToken = default)
        {
            var ghostUsername = await accountServices.RegisterGhostAccount();
            return await accountServices.AuthenticateByUserName(ghostUsername.Data);
        }
    }
}