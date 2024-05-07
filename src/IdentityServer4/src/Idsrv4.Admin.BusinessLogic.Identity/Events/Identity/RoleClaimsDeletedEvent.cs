using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class RoleClaimsDeletedEvent<TRoleClaimsDto> : AuditEvent
{
    public RoleClaimsDeletedEvent(TRoleClaimsDto roleClaim)
    {
        RoleClaim = roleClaim;
    }

    public TRoleClaimsDto RoleClaim { get; set; }
}