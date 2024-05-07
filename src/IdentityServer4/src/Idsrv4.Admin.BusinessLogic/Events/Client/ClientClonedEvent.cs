using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientClonedEvent : AuditEvent
{
    public ClientClonedEvent(ClientCloneDto client)
    {
        Client = client;
    }

    public ClientCloneDto Client { get; set; }
}