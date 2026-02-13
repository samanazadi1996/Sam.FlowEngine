using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using System;
using System.Collections.Generic;

namespace FlowEngine.Application.Features.Accounts.Queries.GetProfile;

public class GetProfileQuery : IRequest<BaseResult<UserDto>>
{
    public string UserName { get; set; }
}