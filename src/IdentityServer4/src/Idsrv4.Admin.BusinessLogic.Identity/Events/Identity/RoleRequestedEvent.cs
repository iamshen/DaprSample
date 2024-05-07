using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class RoleRequestedEvent<TRoleDto> : AuditEvent
{
    public RoleRequestedEvent(TRoleDto role)
    {
        Role = role;
    }

    public TRoleDto Role { get; set; }
}