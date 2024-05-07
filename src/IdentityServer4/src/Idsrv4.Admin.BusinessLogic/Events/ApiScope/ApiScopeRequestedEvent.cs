using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiScope;

public class ApiScopeRequestedEvent : AuditEvent
{
    public ApiScopeRequestedEvent(ApiScopeDto apiScopes)
    {
        ApiScopes = apiScopes;
    }

    public ApiScopeDto ApiScopes { get; set; }
}