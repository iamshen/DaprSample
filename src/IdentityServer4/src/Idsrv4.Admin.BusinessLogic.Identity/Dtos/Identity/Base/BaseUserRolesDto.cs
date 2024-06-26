﻿using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

public class BaseUserRolesDto<TKey> : IBaseUserRolesDto
{
    public TKey UserId { get; set; }

    public TKey RoleId { get; set; }

    object IBaseUserRolesDto.UserId => UserId;

    object IBaseUserRolesDto.RoleId => RoleId;
}