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
        JobParameter_EnvironmentVariable_DataType,

        JobParameter_Execute,
        List,
    }
    public enum JobParameter_HttpResuest_ResponseType
    {
        Json,
        Xml,
        Html,
        Text,
    }
    public enum JobParameter_EnvironmentVariable_DataType
    {
        Json,
        Xml,
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
