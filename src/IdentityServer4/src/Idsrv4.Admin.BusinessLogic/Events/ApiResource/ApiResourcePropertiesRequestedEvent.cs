using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiResource;

public class ApiResourcePropertiesRequestedEvent : AuditEvent
{
    public ApiResourcePropertiesRequestedEvent(int apiResourceId, ApiResourcePropertiesDto apiResourceProperties)
    {
        ApiResourceId = apiResourceId;
        ApiResourceProperties = apiResourceProperties;
    }

    public int ApiResourceId { get; set; }
    public ApiResourcePropertiesDto ApiResourceProperties { get; }
}