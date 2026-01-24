using FlowEngine.Domain.Projects.ValueObjects;
using FlowEngine.Infrastructure.Worker.Core;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FlowEngine.Infrastructure.Worker.Helpers;

internal static class ValuePlaceholderProcessor
{
    public static string GetValue(this ProjectModel projectModel, Dictionary<string, JobParameter> jobParameters, string parameterName)
    {
        var temp = jobParameters[parameterName]?.Value;
        if (string.IsNullOrEmpty(temp))
            return temp;
        try
        {
            var matches = Regex.Matches(temp, @"\$\{([^}]*)\}");
            foreach (Match match in matches)
            {
                var rep = match.Groups[1].Value;
                var dicKey = rep.Split(".")[0];
                var job = projectModel.Jobs.FirstOrDefault(p => p.Name == dicKey);
                if (job is not null)
                {

                    if (projectModel.Data is not null && projectModel.Data.TryGetValue(dicKey, out var data))
                    {
                        var responseType = DataTypeExtensions.DetectFormat(data);
                        if (responseType == DataTypeExtensions.Text)
                        {
                            temp = temp.Replace(match.Value, data);
                        }
                        if (responseType == DataTypeExtensions.Json)
                        {
                            var parts = match.Groups[1].Value.Split('.', StringSplitOptions.RemoveEmptyEntries);

                            using var doc = JsonDocument.Parse(data);
                            var replacement = GetJsonValue(doc.RootElement, [.. parts.Skip(1)]);
                            temp = temp.Replace(match.Value, replacement);
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
        }

        return temp;

        string? GetJsonValue(JsonElement element, string[] path)
        {
            JsonElement current = element;

            foreach (var p in path)
            {
                if (p.EndsWith("]") && p.Contains("["))
                {
                    var idxStart = p.IndexOf('[');
                    var propName = p[..idxStart];
                    var idxStr = p.Substring(idxStart + 1, p.Length - idxStart - 2);
                    if (!int.TryParse(idxStr, out int index))
                        return null;

                    if (!string.IsNullOrEmpty(propName))
                    {
                        if (current.ValueKind == JsonValueKind.Object && current.TryGetProperty(propName, out var next))
                            current = next;
                        else
                            return null;
                    }

                    if (current.ValueKind == JsonValueKind.Array && current.GetArrayLength() > index)
                    {
                        current = current[index];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (current.ValueKind == JsonValueKind.Object && current.TryGetProperty(p, out var next))
                        current = next;
                    else
                        return null;
                }
            }

            return current.ValueKind switch
            {
                JsonValueKind.String => current.GetString(),
                JsonValueKind.Number => current.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                _ => current.GetRawText()
            };
        }

    }


}
