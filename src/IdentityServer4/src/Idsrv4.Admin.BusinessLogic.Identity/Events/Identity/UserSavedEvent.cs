using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserSavedEvent<TUserDto> : AuditEvent
{
    public UserSavedEvent(TUserDto user)
    {
        User = user;
    }

    public TUserDto User { get; set; }
}