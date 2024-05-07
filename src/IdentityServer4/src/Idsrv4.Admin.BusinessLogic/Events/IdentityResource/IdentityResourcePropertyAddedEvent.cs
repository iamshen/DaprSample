using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.IdentityResource;

public class IdentityResourcePropertyAddedEvent : AuditEvent
{
    public IdentityResourcePropertyAddedEvent(IdentityResourcePropertiesDto identityResourceProperty)
    {
        IdentityResourceProperty = identityResourceProperty;
    }

    public IdentityResourcePropertiesDto IdentityResourceProperty { get; set; }
}