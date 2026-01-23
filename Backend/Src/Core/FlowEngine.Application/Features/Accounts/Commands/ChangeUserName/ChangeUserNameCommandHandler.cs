using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.UserInterfaces;
using FlowEngine.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Accounts.Commands.ChangeUserName
{
    public class ChangeUserNameCommandHandler(IAccountServices accountServices) : IRequestHandler<ChangeUserNameCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken = default)
        {
            return await accountServices.ChangeUserName(request);
        }
    }
}