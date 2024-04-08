using Microsoft.Extensions.Logging;

namespace DaprTool.BuildingBlocks.Abstractions.Logging;

public static partial class ApplicationLogging
{
    [LoggerMessage(LogLevel.Information, "DbEvent Handler {EventName} Success; EventId:{EventId}")]
    public static partial void LogDbEventSuccess(this ILogger logger, string eventName, Guid eventId);

    [LoggerMessage(LogLevel.Error, "DbEvent Handler {EventName} Fail; EventId:{EventId}; Message: {Message}")]
    public static partial void LogDbEventFailure(this ILogger logger, string eventName, Guid eventId, string message, Exception exception);
    public static void LogDbEventFailure(this ILogger logger, string eventName, Guid eventId, Exception ex)
    {
        LogDbEventFailure(logger, eventName, eventId, ex.Message, ex);
    }
}