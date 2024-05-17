using System;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Log;

public class LogsDeletedEvent : AuditEvent
{
    public LogsDeletedEvent(DateTime deleteOlderThan)
    {
        DeleteOlderThan = deleteOlderThan;
    }

    public DateTime DeleteOlderThan { get; set; }
}