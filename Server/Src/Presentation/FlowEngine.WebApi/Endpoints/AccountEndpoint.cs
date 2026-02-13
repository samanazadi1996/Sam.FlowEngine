using FlowEngine.Application.DTOs.Account.Responses;
using FlowEngine.Application.Features.Accounts.Commands.Authenticate;
using FlowEngine.Application.Features.Accounts.Commands.ChangePassword;
using FlowEngine.Application.Features.Accounts.Commands.ChangeUserName;
using FlowEngine.Application.Features.Accounts.Commands.Start;
using FlowEngine.Application.Features.Accounts.Queries.GetProfile;
using FlowEngine.Application.Interfaces;
using FlowEngine.Application.Wrappers;
using FlowEngine.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace FlowEngine.WebApi.Endpoints
{
    public class AccountEndpoint : EndpointGroupBase
    {
        public override void Map(RouteGroupBuilder builder)
        {
            builder.MapPost(Authenticate);

            builder.MapPut(ChangeUserName).RequireAuthorization();

            builder.MapPut(ChangePassword).RequireAuthorization();

            builder.MapPost(Start);

            builder.MapGet(GetProfile).RequireAuthorization();
        }

        async Task<BaseResult<AuthenticationResponse>> Authenticate(IMediator mediator, AuthenticateCommand model)
            => await mediator.Send<AuthenticateCommand, BaseResult<AuthenticationResponse>>(model);

        async Task<BaseResult> ChangeUserName(IMediator mediator, ChangeUserNameCommand model)
            => await mediator.Send<ChangeUserNameCommand, BaseResult>(model);

        async Task<BaseResult> ChangePassword(IMediator mediator, ChangePasswordCommand model)
            => await mediator.Send<ChangePasswordCommand, BaseResult>(model);

        async Task<BaseResult<AuthenticationResponse>> Start(IMediator mediator)
            => await mediator.Send<StartCommand, BaseResult<AuthenticationResponse>>(new StartCommand());


        async Task<BaseResult<UserDto>> GetProfile(IMediator mediator)
            => await mediator.Send<GetProfileQuery, BaseResult<UserDto>>(new GetProfileQuery());
    }

}
