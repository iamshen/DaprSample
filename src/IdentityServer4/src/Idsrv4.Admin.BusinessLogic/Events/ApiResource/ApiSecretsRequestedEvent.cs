﻿using System;
using System.Collections.Generic;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.ApiResource;

public class ApiSecretsRequestedEvent : AuditEvent
{
    public ApiSecretsRequestedEvent(int apiResourceId,
        List<(int apiSecretId, string type, DateTime? expiration)> secrets)
    {
        ApiResourceId = apiResourceId;
        Secrets = secrets;
    }

    public int ApiResourceId { get; set; }

    public List<(int apiSecretId, string type, DateTime? expiration)> Secrets { get; set; }
}