using FlowEngine.Application.DTOs.Account.Requests;
using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Wrappers;
using System;
using System.Threading.Tasks;

namespace FlowEngine.Application.Interfaces.UserInterfaces
{
    public interface IGetUserServices
    {
        Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model);
        Task<UserDto> GetById(Guid userId);
    }
}
