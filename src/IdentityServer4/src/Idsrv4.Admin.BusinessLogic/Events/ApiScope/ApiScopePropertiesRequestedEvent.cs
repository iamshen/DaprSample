using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiScope;

public class ApiScopePropertiesRequestedEvent : AuditEvent
{
    public ApiScopePropertiesRequestedEvent(int apiScopeId, ApiScopePropertiesDto apiScopeProperties)
    {
        ApiScopeId = apiScopeId;
        ApiResourceProperties = apiScopeProperties;
    }

    public int ApiScopeId { get; set; }
    public ApiScopePropertiesDto ApiResourceProperties { get; }
}