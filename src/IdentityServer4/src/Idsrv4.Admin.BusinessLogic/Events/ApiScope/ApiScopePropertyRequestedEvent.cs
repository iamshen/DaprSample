using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiScope;

public class ApiScopePropertyRequestedEvent : AuditEvent
{
    public ApiScopePropertyRequestedEvent(int apiScopePropertyId, ApiScopePropertiesDto apiScopeProperty)
    {
        ApiScopePropertyId = apiScopePropertyId;
        ApiScopeProperty = apiScopeProperty;
    }

    public int ApiScopePropertyId { get; set; }

    public ApiScopePropertiesDto ApiScopeProperty { get; set; }
}