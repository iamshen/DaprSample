using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class RoleClaimsRequestedEvent<TRoleClaimsDto> : AuditEvent
{
    public RoleClaimsRequestedEvent(TRoleClaimsDto roleClaims)
    {
        RoleClaims = roleClaims;
    }

    public TRoleClaimsDto RoleClaims { get; set; }
}