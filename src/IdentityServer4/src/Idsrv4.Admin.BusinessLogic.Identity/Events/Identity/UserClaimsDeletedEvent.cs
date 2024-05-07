using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserClaimsDeletedEvent<TUserClaimsDto> : AuditEvent
{
    public UserClaimsDeletedEvent(TUserClaimsDto claim)
    {
        Claim = claim;
    }

    public TUserClaimsDto Claim { get; set; }
}