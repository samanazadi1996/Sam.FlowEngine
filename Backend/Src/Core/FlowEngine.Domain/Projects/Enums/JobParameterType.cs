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
        Xml,
        Html,
        Text,
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
