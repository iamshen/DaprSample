﻿using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Base;
using Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Idsrv4.Admin.BusinessLogic.Identity.Dtos.Identity;

public class UserProviderDto<TKey> : BaseUserProviderDto<TKey>, IUserProviderDto
{
    public string UserName { get; set; }

    public string ProviderKey { get; set; }

    public string LoginProvider { get; set; }

    public string ProviderDisplayName { get; set; }
}