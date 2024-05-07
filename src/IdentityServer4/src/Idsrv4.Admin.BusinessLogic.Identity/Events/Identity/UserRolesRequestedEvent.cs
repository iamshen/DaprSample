using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserRolesRequestedEvent<TUserRolesDto> : AuditEvent
{
    public UserRolesRequestedEvent(TUserRolesDto roles)
    {
        Roles = roles;
    }

    public TUserRolesDto Roles { get; set; }
}