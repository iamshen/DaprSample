namespace WebAdmin.Shared.Infrastructure;

/// <summary>
///     AppVersion Service
/// </summary>
public interface IAppVersionService
{
    string Version { get; }
}