using System.Reflection;

namespace WebAdmin.Infrastructure;

/// <summary>
///     AppVersion Service
/// </summary>
public class AppVersionService : IAppVersionService
{
    public string Version => GetVersionFromAssembly();

    public static string GetVersionFromAssembly()
    {
        string strVersion = default!;
        var versionAttribute =
            Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        if (versionAttribute == null) return strVersion;
        var version = versionAttribute.InformationalVersion;
        var plusIndex = version.IndexOf('+');
        if (plusIndex >= 0 && plusIndex + 9 < version.Length)
            strVersion = version[..(plusIndex + 9)];
        else
            strVersion = version;

        return strVersion;
    }
}