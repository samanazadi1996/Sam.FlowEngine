using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.UserInterfaces;
using FlowEngine.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler(IAccountServices accountServices) : IRequestHandler<ChangePasswordCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken = default)
        {
            return await accountServices.ChangePassword(request);
        }
    }
}