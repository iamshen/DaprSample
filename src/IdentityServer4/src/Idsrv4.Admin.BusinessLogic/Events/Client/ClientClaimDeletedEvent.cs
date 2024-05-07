using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientClaimDeletedEvent : AuditEvent
{
    public ClientClaimDeletedEvent(ClientClaimsDto clientClaim)
    {
        ClientClaim = clientClaim;
    }

    public ClientClaimsDto ClientClaim { get; set; }
}