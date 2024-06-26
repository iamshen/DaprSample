﻿using System.Collections.Generic;

namespace Idsrv4.Admin.BusinessLogic.Dtos.Configuration;

public class ApiScopesDto
{
    public ApiScopesDto()
    {
        Scopes = new List<ApiScopeDto>();
    }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public List<ApiScopeDto> Scopes { get; set; }
}