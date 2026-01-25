using FlowEngine.Domain.Projects.ValueObjects;
using FlowEngine.Infrastructure.Worker.Core;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FlowEngine.Infrastructure.Worker.Helpers;

public static class ValuePlaceholderProcessor
{
    public static string GetValue(this ProjectModel projectModel, Dictionary<string, JobParameter> jobParameters, string parameterName)
    {
        var temp = jobParameters[parameterName]?.Value;
        if (string.IsNullOrEmpty(temp))
            return temp;
        try
        {
            temp = ResolvePlaceholders(temp);

            var matches = Regex.Matches(temp, @"\$\{([^}]*)\}");
            foreach (Match match in matches)
            {
                var rep = match.Groups[1].Value;
                var dicKey = rep.Split(".")[0];

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


        string ResolvePlaceholders(string template)
        {
            var replacements = GetReplacements();
            foreach (var item in replacements)
            {
                template = template.Replace($"${{{item.Key}}}", item.Value, StringComparison.OrdinalIgnoreCase);
            }

            return template;
        }
    }
    public static Dictionary<string, string> GetReplacements()
    {
        return new Dictionary<string, string>()
            {
                { "System.DateTime.Now",DateTime.Now.ToString()},
                { "System.DateTime.Now.Year",DateTime.Now.Year.ToString()},
                { "System.DateTime.Now.Month",DateTime.Now.Month.ToString()},
                { "System.DateTime.Now.Day",DateTime.Now.Day.ToString()},
                { "System.DateTime.Now.Hour",DateTime.Now.Hour.ToString()},
                { "System.DateTime.Now.Minute",DateTime.Now.Minute.ToString()},
                { "System.DateTime.Now.Second",DateTime.Now.Second.ToString()},
                { "System.DateTime.Now.Millisecond",DateTime.Now.Millisecond.ToString()},
                { "System.DateTime.ShortDate", DateTime.Now.ToString("yyyy-MM-dd") },
                { "System.DateTime.LongDate", DateTime.Now.ToString("dddd, MMMM dd, yyyy") },
                { "System.DateTime.ShortTime", DateTime.Now.ToString("HH:mm") },
                { "System.DateTime.LongTime", DateTime.Now.ToString("HH:mm:ss") },
                { "System.DateTime.Timestamp", DateTime.Now.ToString("yyyyMMddHHmmssfff") },
                { "System.DateTime.UnixTimestamp", ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString() },

                { "System.Guid.New", Guid.NewGuid().ToString() },
                { "System.Guid.New.Upper", Guid.NewGuid().ToString().ToUpper() },
                { "System.Guid.New.NoDash", Guid.NewGuid().ToString().Replace("-", string.Empty) },
            };

    }


}
