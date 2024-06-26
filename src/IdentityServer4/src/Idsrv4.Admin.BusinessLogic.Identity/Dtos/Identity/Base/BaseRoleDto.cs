﻿using System.Collections.Generic;
using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

public class BaseRoleDto<TRoleId> : IBaseRoleDto
{
    public TRoleId Id { get; set; }

    public bool IsDefaultId() => EqualityComparer<TRoleId>.Default.Equals(Id, default);

    object IBaseRoleDto.Id => Id;
}