using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiResource;

public class ApiResourcePropertyAddedEvent : AuditEvent
{
    public ApiResourcePropertyAddedEvent(ApiResourcePropertiesDto apiResourceProperty)
    {
        ApiResourceProperty = apiResourceProperty;
    }

    public ApiResourcePropertiesDto ApiResourceProperty { get; set; }
}