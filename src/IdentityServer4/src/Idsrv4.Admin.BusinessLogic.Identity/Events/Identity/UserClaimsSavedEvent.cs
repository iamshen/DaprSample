using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserClaimsSavedEvent<TUserClaimsDto> : AuditEvent
{
    public UserClaimsSavedEvent(TUserClaimsDto claims)
    {
        Claims = claims;
    }

    public TUserClaimsDto Claims { get; set; }
}