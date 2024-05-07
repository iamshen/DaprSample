using System.Collections.Generic;
using Idsrv4.Admin.EntityFramework.Configuration.Configuration.Identity;

namespace Idsrv4.Admin.EntityFramework.Configuration.Configuration;

public class IdentityData
{
    public List<Role> Roles { get; set; }
    public List<User> Users { get; set; }
}