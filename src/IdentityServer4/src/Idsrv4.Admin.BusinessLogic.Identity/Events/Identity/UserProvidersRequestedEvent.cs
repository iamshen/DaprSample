using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserProvidersRequestedEvent<TUserProvidersDto> : AuditEvent
{
    public UserProvidersRequestedEvent(TUserProvidersDto providers)
    {
        Providers = providers;
    }

    public TUserProvidersDto Providers { get; set; }
}