using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserClaimsRequestedEvent<TUserClaimsDto> : AuditEvent
{
    public UserClaimsRequestedEvent(TUserClaimsDto claims)
    {
        Claims = claims;
    }

    public TUserClaimsDto Claims { get; set; }
}