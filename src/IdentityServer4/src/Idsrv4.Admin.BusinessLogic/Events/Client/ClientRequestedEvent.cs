using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientRequestedEvent : AuditEvent
{
    public ClientRequestedEvent(ClientDto clientDto)
    {
        ClientDto = clientDto;
    }

    public ClientDto ClientDto { get; set; }
}