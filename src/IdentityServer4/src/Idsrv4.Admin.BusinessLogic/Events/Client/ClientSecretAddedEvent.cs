﻿using System;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.Client;

public class ClientSecretAddedEvent : AuditEvent
{
    public ClientSecretAddedEvent(int clientId, string type, DateTime? expiration)
    {
        ClientId = clientId;
        Type = type;
        Expiration = expiration;
    }

    public string Type { get; set; }

    public DateTime? Expiration { get; set; }

    public int ClientId { get; set; }
}