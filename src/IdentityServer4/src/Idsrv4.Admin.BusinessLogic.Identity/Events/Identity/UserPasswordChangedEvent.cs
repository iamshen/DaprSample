using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserPasswordChangedEvent : AuditEvent
{
    public UserPasswordChangedEvent(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
}