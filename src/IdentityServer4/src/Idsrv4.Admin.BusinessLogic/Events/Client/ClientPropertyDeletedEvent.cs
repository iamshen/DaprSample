using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientPropertyDeletedEvent : AuditEvent
{
    public ClientPropertyDeletedEvent(ClientPropertiesDto clientProperty)
    {
        ClientProperty = clientProperty;
    }

    public ClientPropertiesDto ClientProperty { get; set; }
}