using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiResource;

public class ApiResourceRequestedEvent : AuditEvent
{
    public ApiResourceRequestedEvent(int apiResourceId, ApiResourceDto apiResource)
    {
        ApiResourceId = apiResourceId;
        ApiResource = apiResource;
    }

    public int ApiResourceId { get; set; }
    public ApiResourceDto ApiResource { get; set; }
}