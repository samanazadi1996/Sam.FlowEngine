using FlowEngine.Application.Helpers;
using FlowEngine.Application.Wrappers;
using FlowEngine.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace FlowEngine.WebApi.Endpoints;

public class TestEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapGet(GetJson);

        builder.MapPost(PostJson);
        builder.MapPost(PostJsonRequireAuthorization).RequireAuthorization();
    }

    object GetJson(string parameter)
    {
        return BaseResult<object>.Ok(new
        {
            Text = RandomHelper.RandomString(10),
            Number = RandomHelper.RandomInt(1, 5000),
            Boolean = RandomHelper.RandomBool(),
            Parameter = parameter
        });
    }

    BaseResult<object> PostJson(object data)
    {
        return BaseResult<object>.Ok(new
        {
            Authorization = false,
            Data = data
        });
    }

    BaseResult<object> PostJsonRequireAuthorization(object data)
    {
        return BaseResult<object>.Ok(new
        {
            Authorization = true,
            Data = data
        });
    }
}
