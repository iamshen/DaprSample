﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Idsrv4.Admin.BusinessLogic.Dtos.Configuration;

public class IdentityResourcePropertiesDto
{
    public int IdentityResourcePropertyId { get; set; }

    public int IdentityResourceId { get; set; }

    public string IdentityResourceName { get; set; }

    [Required] public string Key { get; set; }

    [Required] public string Value { get; set; }

    public List<IdentityResourcePropertyDto> IdentityResourceProperties { get; set; } = new();

    public int TotalCount { get; set; }

    public int PageSize { get; set; }
}