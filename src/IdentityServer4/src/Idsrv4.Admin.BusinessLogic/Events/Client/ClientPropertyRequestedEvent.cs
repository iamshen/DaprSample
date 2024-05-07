using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientPropertyRequestedEvent : AuditEvent
{
    public ClientPropertyRequestedEvent(ClientPropertiesDto clientProperties)
    {
        ClientProperties = clientProperties;
    }

    public ClientPropertiesDto ClientProperties { get; set; }
}