using Idsrv4.Admin.BusinessLogic.Dtos.Grant;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.PersistedGrant;

public class PersistedGrantRequestedEvent : AuditEvent
{
    public PersistedGrantRequestedEvent(PersistedGrantDto persistedGrant)
    {
        PersistedGrant = persistedGrant;
    }

    public PersistedGrantDto PersistedGrant { get; set; }
}