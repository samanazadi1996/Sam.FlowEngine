using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Enums;
using FlowEngine.Infrastructure.Worker.Helpers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace FlowEngine.Infrastructure.Worker.Core.Jobs;

public sealed class Job_HttpRequest : IJob
{
    public Job_HttpRequest()
    {
        ClassName = this.GetType().FullName!;
        JobParameters = new()
        {
            { FlowEngineConst.Method,new(JobParameterType.JobParameter_HttpResuest_MethodType,nameof(JobParameter_HttpResuest_MethodType.Get))},
            { FlowEngineConst.Url,new(JobParameterType.String)},
            { FlowEngineConst.Body,new(JobParameterType.String)},
            { FlowEngineConst.ResponseType,new(JobParameterType.JobParameter_HttpResuest_ResponseType,nameof(JobParameter_HttpResuest_ResponseType.Json))},
        };
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        var url = projectModel.GetValue(JobParameters, FlowEngineConst.Url);
        var body = projectModel.GetValue(JobParameters, FlowEngineConst.Body);
        var responseType = projectModel.GetValue(JobParameters, FlowEngineConst.ResponseType);
        var method = GetHttpMethod(projectModel);

        var client = new HttpClient();

        var request = new HttpRequestMessage(method, url)
        {
            Content = string.IsNullOrEmpty(body) ? null : new StringContent(body, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await client.SendAsync(request);

        var responseContent = await response.Content.ReadAsStringAsync();

        projectModel.Data ??= [];

        projectModel.Data[this.Name] = Enum.Parse<JobParameter_HttpResuest_ResponseType>(responseType) switch
        {
            JobParameter_HttpResuest_ResponseType.Json => BuildJsonResponse(response, responseContent),
            JobParameter_HttpResuest_ResponseType.Xml => BuildXmlResponse(response, responseContent),
            JobParameter_HttpResuest_ResponseType.Text => BuildTextResponse(response, responseContent),
            _ => BuildTextResponse(response, responseContent)
        };

        ConsoleLogger.Log($"Send HttpRequest {(int)response.StatusCode} {url}");

        await GotoNextJob(projectModel, this.NextJob);
    }


    private string BuildJsonResponse(HttpResponseMessage response, string data)
    {
        return JsonSerializer.Serialize(new
        {
            Status = (int)response.StatusCode,
            Data = JsonSerializer.Deserialize<object>(data)
        });
    }

    private string BuildXmlResponse(HttpResponseMessage response, string data)
    {
        XElement dataElement;

        try
        {
            dataElement = XElement.Parse(data);
        }
        catch
        {
            dataElement = new XElement("Raw", new XCData(data));
        }

        var document = new XDocument(
            new XElement("Response",
                new XElement("Status", (int)response.StatusCode),
                new XElement("Data", dataElement)
            )
        );

        return document.ToString(SaveOptions.DisableFormatting);
    }
    private string BuildTextResponse(HttpResponseMessage response, string data)
    {
        return JsonSerializer.Serialize(new
        {
            Status = (int)response.StatusCode,
            Data = data
        });
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
