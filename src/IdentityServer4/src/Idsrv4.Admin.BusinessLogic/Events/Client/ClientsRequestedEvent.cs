using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientsRequestedEvent : AuditEvent
{
    public ClientsRequestedEvent(ClientsDto clientsDto)
    {
        ClientsDto = clientsDto;
    }

    public ClientsDto ClientsDto { get; set; }
}