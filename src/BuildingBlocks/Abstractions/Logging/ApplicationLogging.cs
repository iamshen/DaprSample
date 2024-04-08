using Microsoft.Extensions.Logging;

namespace DaprTool.BuildingBlocks.Abstractions.Logging;

public static partial class ApplicationLogging
{
    [LoggerMessage(LogLevel.Information, "DbEvent Handler {EventName} Success; EventId:{EvtId}")]
    private static partial void LogDbEventSuccess(this ILogger logger, string eventName, string evtId);
    public static void LogDbEventSuccess(this ILogger logger, string eventName, Guid eventId)
    {
        LogDbEventSuccess(logger, eventName, eventId.ToString());
    }




    [LoggerMessage(LogLevel.Error, "DbEvent Handler {EventName} Fail; EventId:{EvtId}; Message: {Message}")]
    private static partial void LogDbEventFailure(this ILogger logger, string eventName, string evtId, string message, Exception exception);
    public static void LogDbEventFailure(this ILogger logger, string eventName, Guid eventId, Exception ex)
    {
        LogDbEventFailure(logger, eventName, eventId.ToString(), ex.Message, ex);
    }
}