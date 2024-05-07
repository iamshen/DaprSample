using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiResource;

public class ApiResourceUpdatedEvent : AuditEvent
{
    public ApiResourceUpdatedEvent(ApiResourceDto originalApiResource, ApiResourceDto apiResource)
    {
        OriginalApiResource = originalApiResource;
        ApiResource = apiResource;
    }

    public ApiResourceDto OriginalApiResource { get; set; }
    public ApiResourceDto ApiResource { get; set; }
}