using System;
using System.Collections.Generic;
using System.Text;

namespace FlowEngine.Domain.Projects.Enums
{
    public enum JobParameterType
    {
        //@FromData,
        @Int,
        @String,
        @Bool,
        @Long,
        @Double,
        @Float,

        JobParameter_HttpResuest_ResponseType,
        JobParameter_HttpResuest_MethodType,

        JobParameter_Execute,
    }
    public enum JobParameter_HttpResuest_ResponseType
    {
        Json,
        Html,
        Csv,
        @Int,
        @String,
        @Bool,
        @Long,
        @Double,
        @Float,
    }
    public enum JobParameter_HttpResuest_MethodType
    {
        Get,
        Post,
        Put,
        Delete,

        Patch,
        Options,
        Head,
    }
}
