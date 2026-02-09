using FlowEngine.Infrastructure.Worker.Core;
using FlowEngine.Infrastructure.Worker.Core.Jobs;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace FlowEngine.Infrastructure.Worker.Helpers;

public static class ValuePlaceholderProcessor
{
    public static string GetValue(this ProjectModel projectModel, Dictionary<string, string?> jobParameters, string parameterName)
    {
        var temp = jobParameters[parameterName];
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
                    else if (responseType == DataTypeExtensions.Json)
                    {
                        var parts = match.Groups[1].Value.Split('.', StringSplitOptions.RemoveEmptyEntries);

                        using var doc = JsonDocument.Parse(data);
                        var replacement = GetJsonValue(doc.RootElement, [.. parts.Skip(1)], projectModel);
                        temp = temp.Replace(match.Value, replacement);
                    }
                    else if (responseType == DataTypeExtensions.Xml)
                    {
                        var parts = match.Groups[1].Value.Split('.', StringSplitOptions.RemoveEmptyEntries);

                        var doc = XDocument.Parse(data);
                        var replacement = GetXmlValue(doc.Root!, [.. parts.Skip(1)]);
                        temp = temp.Replace(match.Value, replacement);
                    }

                }

            }
        }
        catch (Exception)
        {
        }

        return temp;

        string? GetJsonValue(JsonElement element, string[] path, ProjectModel projectModel)
        {
            JsonElement current = element;

            foreach (var p in path)
            {
                if (p.Equals("Length()", StringComparison.OrdinalIgnoreCase))
                {
                    return current.GetArrayLength() + "";
                }
                else if (p.EndsWith("]") && p.Contains("["))
                {
                    var idxStart = p.IndexOf('[');
                    var propName = p[..idxStart];
                    var idxStr = p.Substring(idxStart + 1, p.Length - idxStart - 2);
                    if (!int.TryParse(idxStr, out var index))
                    {
                        if (!idxStr.Contains(">"))
                            return null;

                        index = int.Parse(projectModel.GetValue(
                            new Dictionary<string, string?>()
                            {
                            { "Temp","${"+idxStr.Replace(">",".")+"}" }
                            },
                            "Temp"));

                    }

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

        string? GetXmlValue(XElement element, string[] path)
        {
            XElement? current = element;

            foreach (var p in path)
            {
                if (current == null)
                    return null;

                if (p.StartsWith("@"))
                {
                    return current.Attribute(p[1..])?.Value;
                }

                if (p.EndsWith("]") && p.Contains("["))
                {
                    var idxStart = p.IndexOf('[');
                    var name = p[..idxStart];
                    var idxStr = p.Substring(idxStart + 1, p.Length - idxStart - 2);

                    if (!int.TryParse(idxStr, out var index))
                        return null;

                    current = current.Elements(name).ElementAtOrDefault(index);
                }
                else
                {
                    current = current.Element(p);
                }
            }

            return current?.Value;
        }


        string ResolvePlaceholders(string template)
        {
            var replacements = GetReplacements();
            foreach (var item in replacements)
            {
                template = template.Replace($"${{{"System." + item.Key}}}", item.Value, StringComparison.OrdinalIgnoreCase);
            }

            return template;
        }
    }
    public static Dictionary<string, string> GetReplacements()
    {
        return new Dictionary<string, string>()
            {
                { "DateTime.Now",DateTime.Now.ToString()},
                { "DateTime.Now.Year",DateTime.Now.Year.ToString()},
                { "DateTime.Now.Month",DateTime.Now.Month.ToString()},
                { "DateTime.Now.Day",DateTime.Now.Day.ToString()},
                { "DateTime.Now.Hour",DateTime.Now.Hour.ToString()},
                { "DateTime.Now.Minute",DateTime.Now.Minute.ToString()},
                { "DateTime.Now.Second",DateTime.Now.Second.ToString()},
                { "DateTime.Now.Millisecond",DateTime.Now.Millisecond.ToString()},
                { "DateTime.ShortDate", DateTime.Now.ToString("yyyy-MM-dd") },
                { "DateTime.LongDate", DateTime.Now.ToString("dddd, MMMM dd, yyyy") },
                { "DateTime.ShortTime", DateTime.Now.ToString("HH:mm") },
                { "DateTime.LongTime", DateTime.Now.ToString("HH:mm:ss") },
                { "DateTime.Timestamp", DateTime.Now.ToString("yyyyMMddHHmmssfff") },
                { "DateTime.UnixTimestamp", ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString() },

                { "Guid.New", Guid.NewGuid().ToString() },
                { "Guid.New.Upper", Guid.NewGuid().ToString().ToUpper() },
                { "Guid.New.NoDash", Guid.NewGuid().ToString().Replace("-", string.Empty) },
            };

    }


}
