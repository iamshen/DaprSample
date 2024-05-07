using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.IdentityResource;

public class IdentityResourceRequestedEvent : AuditEvent
{
    public IdentityResourceRequestedEvent(IdentityResourceDto identityResource)
    {
        IdentityResource = identityResource;
    }

    public IdentityResourceDto IdentityResource { get; set; }
}