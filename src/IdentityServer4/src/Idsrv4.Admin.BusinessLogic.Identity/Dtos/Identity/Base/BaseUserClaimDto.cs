﻿using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;

public class BaseUserClaimDto<TUserId> : IBaseUserClaimDto
{
    public TUserId UserId { get; set; }
    public int ClaimId { get; set; }

    object IBaseUserClaimDto.UserId => UserId;
}