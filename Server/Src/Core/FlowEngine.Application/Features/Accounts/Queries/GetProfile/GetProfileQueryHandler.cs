using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Interfaces.UserInterfaces;
using FlowEngine.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlowEngine.Application.Features.Accounts.Queries.GetProfile;

public class GetProfileQueryHandler(IAuthenticatedUserService authenticatedUser, IGetUserServices getUserServices) : IRequestHandler<GetProfileQuery, BaseResult<UserDto>>
{
    public async Task<BaseResult<UserDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await getUserServices.GetById(Guid.Parse(authenticatedUser.UserId));

        return user;
    }
}