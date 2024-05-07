using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Grant;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.PersistedGrant;

public class PersistedGrantsIdentityByUsersRequestedEvent : AuditEvent
{
    public PersistedGrantsIdentityByUsersRequestedEvent(PersistedGrantsDto persistedGrants)
    {
        PersistedGrants = persistedGrants;
    }

    public PersistedGrantsDto PersistedGrants { get; set; }
}