using System.Collections.Generic;
using Idsrv4.Admin.EntityFramework.Configuration.Configuration.Identity;

namespace Idsrv4.Admin.EntityFramework.Configuration.Configuration.IdentityServer;

public class Client : global::IdentityServer4.Models.Client
{
    public List<Claim> ClientClaims { get; set; } = new();
}