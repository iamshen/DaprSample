﻿using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientUpdatedEvent : AuditEvent
{
    public ClientUpdatedEvent(ClientDto originalClient, ClientDto client)
    {
        OriginalClient = originalClient;
        Client = client;
    }

    public ClientDto OriginalClient { get; set; }
    public ClientDto Client { get; set; }
}