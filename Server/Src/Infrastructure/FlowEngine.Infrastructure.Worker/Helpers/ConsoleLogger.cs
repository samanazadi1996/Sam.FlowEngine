namespace FlowEngine.Infrastructure.Worker.Helpers;


public static class ConsoleLogger
{
    public enum LogLevel
    {
        Debug = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
        Critical = 4
    }

    private static readonly object _lock = new();
    private static LogLevel _minimumLogLevel = LogLevel.Information;

    public static LogLevel MinimumLogLevel
    {
        get => _minimumLogLevel;
        set => _minimumLogLevel = value;
    }

    public static void Log(string message) => Log(LogLevel.Information, message);

    public static void Log(LogLevel level, string message)
    {
        if (level < _minimumLogLevel) return;

        var color = GetColorForLevel(level);
        var prefix = GetPrefixForLevel(level);
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        lock (_lock)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{timestamp} [{prefix}] {message}");
            Console.ForegroundColor = originalColor;
        }
    }

    public static void LogDebug(string message) => Log(LogLevel.Debug, message);
    public static void LogInfo(string message) => Log(LogLevel.Information, message);
    public static void LogWarning(string message) => Log(LogLevel.Warning, message);
    public static void LogError(string message) => Log(LogLevel.Error, message);
    public static void LogCritical(string message) => Log(LogLevel.Critical, message);

    public static void LogError(string message, Exception exception)
    {
        Log(LogLevel.Error, $"{message}{Environment.NewLine}{exception}");
    }

    public static void LogCritical(string message, Exception exception)
    {
        Log(LogLevel.Critical, $"{message}{Environment.NewLine}{exception}");
    }

    private static ConsoleColor GetColorForLevel(LogLevel level)
    {
        return level switch
        {
            LogLevel.Debug => ConsoleColor.Gray,
            LogLevel.Information => ConsoleColor.White,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Critical => ConsoleColor.DarkRed,
            _ => ConsoleColor.White
        };
    }

    private static string GetPrefixForLevel(LogLevel level)
    {
        return level switch
        {
            LogLevel.Debug => "DBG",
            LogLevel.Information => "INF",
            LogLevel.Warning => "WRN",
            LogLevel.Error => "ERR",
            LogLevel.Critical => "CRT",
            _ => "INF"
        };
    }
}
