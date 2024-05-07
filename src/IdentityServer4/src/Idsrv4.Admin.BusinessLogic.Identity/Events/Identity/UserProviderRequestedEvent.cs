using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserProviderRequestedEvent<TUserProviderDto> : AuditEvent
{
    public UserProviderRequestedEvent(TUserProviderDto provider)
    {
        Provider = provider;
    }

    public TUserProviderDto Provider { get; set; }
}