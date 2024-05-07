using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class RolesRequestedEvent<TRolesDto> : AuditEvent
{
    public RolesRequestedEvent(TRolesDto roles)
    {
        Roles = roles;
    }

    public TRolesDto Roles { get; set; }
}