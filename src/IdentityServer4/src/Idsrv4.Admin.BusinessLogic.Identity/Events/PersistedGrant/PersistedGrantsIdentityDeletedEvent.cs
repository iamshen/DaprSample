using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.PersistedGrant;

public class PersistedGrantsIdentityDeletedEvent : AuditEvent
{
    public PersistedGrantsIdentityDeletedEvent(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}