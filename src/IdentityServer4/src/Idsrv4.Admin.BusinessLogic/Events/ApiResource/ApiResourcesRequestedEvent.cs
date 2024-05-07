using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiResource;

public class ApiResourcesRequestedEvent : AuditEvent
{
    public ApiResourcesRequestedEvent(ApiResourcesDto apiResources)
    {
        ApiResources = apiResources;
    }

    public ApiResourcesDto ApiResources { get; set; }
}