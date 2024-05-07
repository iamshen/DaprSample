using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserClaimRequestedEvent<TUserClaimsDto> : AuditEvent
{
    public UserClaimRequestedEvent(TUserClaimsDto userClaims)
    {
        UserClaims = userClaims;
    }

    public TUserClaimsDto UserClaims { get; set; }
}