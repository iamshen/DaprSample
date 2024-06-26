﻿using Idsrv4.Admin.BusinessLogic.Dtos.Configuration;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Events.IdentityResource;

public class IdentityResourcesRequestedEvent : AuditEvent
{
    public IdentityResourcesRequestedEvent(IdentityResourcesDto identityResources)
    {
        IdentityResources = identityResources;
    }

    public IdentityResourcesDto IdentityResources { get; }
}