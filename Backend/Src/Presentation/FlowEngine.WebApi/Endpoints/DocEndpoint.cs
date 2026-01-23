using FlowEngine.Application.Wrappers;
using FlowEngine.Domain.Common;
using FlowEngine.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowEngine.WebApi.Endpoints
{
    public class DocEndpoint : EndpointGroupBase
    {
        public override void Map(RouteGroupBuilder builder)
        {
            builder.MapGet(GetErrorCodes);
            builder.MapGet(GetDomainEnums);
        }

        BaseResult<Dictionary<int, string>> GetErrorCodes()
            => Enum.GetValues<ErrorCode>().ToDictionary(t => (int)t, t => t.ToString());
        BaseResult<Dictionary<string, Dictionary<string,string>>> GetDomainEnums()
        {
            
            var assembly=typeof(BaseEntity).Assembly;

           var data= assembly.GetTypes().Where(p => p.IsEnum).Select(p =>new
           {
               Name=p.Name,
               Data=           Enum.GetValues(p).Cast<Enum>().ToDictionary(t => t.ToString(), t => t.ToString())
           }
           );
            return data.ToDictionary(p=>p.Name,x=>x.Data);
        }

    }
}
