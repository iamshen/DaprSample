using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientClaimsRequestedEvent : AuditEvent
{
    public ClientClaimsRequestedEvent(ClientClaimsDto clientClaims)
    {
        ClientClaims = clientClaims;
    }

    public ClientClaimsDto ClientClaims { get; set; }
}