using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.UserInterfaces;
using FlowEngine.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Accounts.Commands.Authenticate
{
    public class AuthenticateCommandHandler(IAccountServices accountServices) : IRequestHandler<AuthenticateCommand, BaseResult<AuthenticationResponse>>
    {
        public async Task<BaseResult<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken = default)
        {
            return await accountServices.Authenticate(request);
        }
    }
}