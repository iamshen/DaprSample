﻿using Idsrv4.Admin.AuditLogging.Events;

namespace Idsrv4.Admin.BusinessLogic.Identity.Events.Identity;

public class UserRoleSavedEvent<TUserRolesDto> : AuditEvent
{
    public UserRoleSavedEvent(TUserRolesDto role)
    {
        Role = role;
    }

    public TUserRolesDto Role { get; set; }
}