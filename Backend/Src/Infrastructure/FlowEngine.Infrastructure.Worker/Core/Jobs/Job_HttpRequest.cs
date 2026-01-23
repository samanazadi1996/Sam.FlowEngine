using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;
using System.Text.Json;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_HttpRequest : IJob
{
    public Job_HttpRequest()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.Method,new(JobParameterType.JobParameter_HttpResuest_MethodType,JobParameter_HttpResuest_MethodType.Get.ToString())},
            { FlowEngineConst.Url,new(JobParameterType.String)},
            { FlowEngineConst.Body,new(JobParameterType.String)},
            { FlowEngineConst.ResponseType,new(JobParameterType.JobParameter_HttpResuest_ResponseType,JobParameter_HttpResuest_ResponseType.Json.ToString())},
        };
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        var url = projectModel.GetValue(JobParameters, FlowEngineConst.Url);
        var body = projectModel.GetValue(JobParameters, FlowEngineConst.Body);
        var method = GetHttpMethod(projectModel);

        var client = new HttpClient();

        var request = new HttpRequestMessage(method, url);

        HttpResponseMessage response = await client.SendAsync(request);

        projectModel.Data ??= [];
        projectModel.Data[this.Name] = JsonSerializer.Serialize(new
        {
            Status = (int)response.StatusCode,
            Data = JsonSerializer.Deserialize<object>(await response.Content.ReadAsStringAsync())
        });

        ConsoleLogger.Log($"Send HttpRequest {(int)response.StatusCode} {url}");

        await GotoNextJob(projectModel, this.NextJob);
    }
    private HttpMethod GetHttpMethod(ProjectModel projectModel)
    {
        var methodName = projectModel.GetValue(JobParameters, FlowEngineConst.Method);

        return Enum.Parse<JobParameter_HttpResuest_MethodType>(methodName) switch
        {
            JobParameter_HttpResuest_MethodType.Get => HttpMethod.Get,
            JobParameter_HttpResuest_MethodType.Post => HttpMethod.Post,
            JobParameter_HttpResuest_MethodType.Put => HttpMethod.Put,
            JobParameter_HttpResuest_MethodType.Delete => HttpMethod.Delete,

            JobParameter_HttpResuest_MethodType.Patch => HttpMethod.Patch,
            JobParameter_HttpResuest_MethodType.Options => HttpMethod.Options,
            JobParameter_HttpResuest_MethodType.Head => HttpMethod.Head,

            _ => HttpMethod.Get,
        };
    }
}
