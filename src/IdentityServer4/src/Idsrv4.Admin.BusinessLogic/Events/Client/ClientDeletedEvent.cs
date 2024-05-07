using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientDeletedEvent : AuditEvent
{
    public ClientDeletedEvent(ClientDto client)
    {
        Client = client;
    }

    public ClientDto Client { get; set; }
}