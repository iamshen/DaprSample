using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.IdentityResource;

public class IdentityResourceUpdatedEvent : AuditEvent
{
    public IdentityResourceUpdatedEvent(IdentityResourceDto originalIdentityResource,
        IdentityResourceDto identityResource)
    {
        OriginalIdentityResource = originalIdentityResource;
        IdentityResource = identityResource;
    }

    public IdentityResourceDto OriginalIdentityResource { get; set; }
    public IdentityResourceDto IdentityResource { get; set; }
}