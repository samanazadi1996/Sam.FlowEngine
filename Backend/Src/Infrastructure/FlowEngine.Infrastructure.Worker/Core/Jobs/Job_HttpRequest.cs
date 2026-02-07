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
            { FlowEngineConst.Method,nameof(JobParameter_HttpResuest_MethodType.Get)},
            { FlowEngineConst.Url,null},
            { FlowEngineConst.Body,null},
            { FlowEngineConst.Headers,null},
        };
    }

    public override async Task Execute(ProjectModel projectModel)
    {
        try
        {
            var url = projectModel.GetValue(JobParameters, FlowEngineConst.Url);
            var body = projectModel.GetValue(JobParameters, FlowEngineConst.Body);
            var headers = projectModel.GetValue(JobParameters, FlowEngineConst.Headers);
            var method = GetHttpMethod(projectModel);

            var client = new HttpClient();

            var request = new HttpRequestMessage(method, url)
            {
                Content = string.IsNullOrEmpty(body) ? null : new StringContent(body, Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrWhiteSpace(headers))
            {
                var dicHeadres = JsonSerializer.Deserialize<Dictionary<string, string>>(headers);
                if (dicHeadres is not null)
                {
                    foreach (var item in dicHeadres)
                    {
                        if (!request.Headers.TryAddWithoutValidation(item.Key, item.Value))
                        {
                            request.Content?.Headers.TryAddWithoutValidation(item.Key, item.Value);
                        }
                    }
                }
            }

            HttpResponseMessage response = await client.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            projectModel.Data ??= [];
            projectModel.Data[this.Name] = DataTypeExtensions.DetectFormat(responseContent) switch
            {
                DataTypeExtensions.Json => BuildJsonResponse(response, responseContent),
                DataTypeExtensions.Xml => BuildXmlResponse(response, responseContent),
                DataTypeExtensions.Text => BuildTextResponse(response, responseContent),
                _ => BuildTextResponse(response, responseContent)
            };

            ConsoleLogger.Log($"Send HttpRequest {(int)response.StatusCode} {url}");

            await GotoNextJob(projectModel, this.NextJob);
        }
        catch (Exception ex)
        {
            ConsoleLogger.LogError(ex.Message);
        }
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

        dataElement = XElement.Parse(data);

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

        return HttpMethod.Parse(methodName);
    }
}
