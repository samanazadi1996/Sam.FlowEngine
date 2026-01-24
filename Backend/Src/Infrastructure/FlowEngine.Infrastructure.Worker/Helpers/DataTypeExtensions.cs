using System.Text.Json;
using System.Xml.Linq;

namespace FlowEngine.Infrastructure.Worker.Helpers;

public static class DataTypeExtensions
{
    internal const string Text = "Text";
    internal const string Json = "Json";
    internal const string Xml = "Xml";
    public static string DetectFormat(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Text;

        input = input.Trim();

        // Try JSON
        if (IsJson(input))
            return Json;

        // Try XML
        if (IsXml(input))
            return Xml;

        return Text;
    }

    private static bool IsJson(string input)
    {
        try
        {
            JsonDocument.Parse(input);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsXml(string input)
    {
        try
        {
            XDocument.Parse(input);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
