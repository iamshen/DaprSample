using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiScope;

public class ApiScopeDeletedEvent : AuditEvent
{
    public ApiScopeDeletedEvent(ApiScopeDto apiScope)
    {
        ApiScope = apiScope;
    }

    public ApiScopeDto ApiScope { get; set; }
}