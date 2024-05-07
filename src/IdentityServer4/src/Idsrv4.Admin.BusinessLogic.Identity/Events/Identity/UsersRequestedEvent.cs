using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UsersRequestedEvent<TUsersDto> : AuditEvent
{
    public UsersRequestedEvent(TUsersDto users)
    {
        Users = users;
    }

    public TUsersDto Users { get; set; }
}