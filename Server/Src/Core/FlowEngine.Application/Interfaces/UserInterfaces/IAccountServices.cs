using FlowEngine.Application.DTOs.Account.Requests;
using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Wrappers;
using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces.UserInterfaces
{
    public interface IAccountServices
    {
        Task<BaseResult<string>> RegisterGhostAccount();
        Task<BaseResult> ChangePassword(ChangePasswordRequest model);
        Task<BaseResult> ChangeUserName(ChangeUserNameRequest model);
        Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
        Task<BaseResult<AuthenticationResponse>> AuthenticateByUserName(string username);

    }
}
