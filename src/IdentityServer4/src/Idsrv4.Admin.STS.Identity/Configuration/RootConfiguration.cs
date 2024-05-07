using Idsrv4.Admin.Shared.Configuration.Configuration.Identity;

namespace Idsrv4.Admin.STS.Identity.Configuration;

public class RootConfiguration : IRootConfiguration
{
    public AdminConfiguration AdminConfiguration { get; } = new();
    public RegisterConfiguration RegisterConfiguration { get; } = new();
}