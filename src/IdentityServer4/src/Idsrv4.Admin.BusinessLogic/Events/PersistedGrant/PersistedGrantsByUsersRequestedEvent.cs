using Idsrv4.Admin.BusinessLogic.Dtos.Grant;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.PersistedGrant;

public class PersistedGrantsByUsersRequestedEvent : AuditEvent
{
    public PersistedGrantsByUsersRequestedEvent(PersistedGrantsDto persistedGrants)
    {
        PersistedGrants = persistedGrants;
    }

    public PersistedGrantsDto PersistedGrants { get; set; }
}