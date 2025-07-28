#if DEBUG
using Serilog;
#endif

public static class AppLogger
{
#if DEBUG
    public static ILogger Logger = new LoggerConfiguration()
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
        .MinimumLevel.Debug()
        .CreateLogger();
#else
    public static ILogger Logger = null;
#endif

    public static void Log(string message)
    {
#if DEBUG
        Logger?.Debug(message);
#endif
    }
}
