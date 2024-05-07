using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.PersistedGrant;

public class PersistedGrantDeletedEvent : AuditEvent
{
    public PersistedGrantDeletedEvent(string persistedGrantKey)
    {
        PersistedGrantKey = persistedGrantKey;
    }

    public string PersistedGrantKey { get; set; }
}