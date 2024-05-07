using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class RoleClaimsSavedEvent<TRoleClaimsDto> : AuditEvent
{
    public RoleClaimsSavedEvent(TRoleClaimsDto claims)
    {
        Claims = claims;
    }

    public TRoleClaimsDto Claims { get; set; }
}