namespace FlowEngine.Domain.Projects.Enums
{
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

        Query,
        Connect,
        Trace,

    }
}
