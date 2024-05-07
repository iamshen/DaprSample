using Idsrv4.Admin.Shared.Configuration.Configuration.Identity;

namespace Idsrv4.Admin.STS.Identity.Configuration.Interfaces;

public interface IRootConfiguration
{
    AdminConfiguration AdminConfiguration { get; }

    RegisterConfiguration RegisterConfiguration { get; }
}