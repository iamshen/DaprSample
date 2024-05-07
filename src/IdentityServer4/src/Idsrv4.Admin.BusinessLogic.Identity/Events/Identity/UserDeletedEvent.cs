using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserDeletedEvent<TUserDto> : AuditEvent
{
    public UserDeletedEvent(TUserDto user)
    {
        User = user;
    }

    public TUserDto User { get; set; }
}