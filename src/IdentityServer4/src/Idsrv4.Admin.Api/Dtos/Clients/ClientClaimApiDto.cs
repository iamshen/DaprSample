﻿using System.ComponentModel.DataAnnotations;

namespace Idsrv4.Admin.Api.Dtos.Clients;

public class ClientClaimApiDto
{
    public int Id { get; set; }

    [Required] public string Type { get; set; }

    [Required] public string Value { get; set; }
}