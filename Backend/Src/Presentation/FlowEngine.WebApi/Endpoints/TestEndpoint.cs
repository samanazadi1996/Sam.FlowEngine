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
    }

    BaseResult<object> GetJson(string parameter)
    {
        return BaseResult<object>.Ok(new
        {
            Text = RandomHelper.RandomString(10),
            Number = RandomHelper.RandomInt(1, 5000),
            Boolean = RandomHelper.RandomBool(),
            Parameter = parameter
        });
    }
}
