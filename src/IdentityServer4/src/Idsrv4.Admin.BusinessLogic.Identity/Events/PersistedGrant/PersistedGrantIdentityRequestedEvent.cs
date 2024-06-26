﻿using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Grant;
using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.PersistedGrant;

public class PersistedGrantIdentityRequestedEvent : AuditEvent
{
    public PersistedGrantIdentityRequestedEvent(PersistedGrantDto persistedGrant)
    {
        PersistedGrant = persistedGrant;
    }

    public PersistedGrantDto PersistedGrant { get; set; }
}