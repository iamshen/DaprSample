using DaprTool.BuildingBlocks.Utils.ValueObjects;

namespace DaprTool.BuildingBlocks.Utils.Constant;

/// <summary />
public static class Constants
{
    /// <summary />
    public const string ResourcesPath = "../../dapr/components";
    /// <summary />
    public const string PubSubName = "dt-pubsub";
    /// <summary />
    public const string SecretStore = "dt-secretstore";
    /// <summary />
    public const string StateStore = "dt-statestore";
    /// <summary />
    public const string ConfigurationStore = "dt-configurationstore";
    /// <summary />
    public static readonly string DefaultActorId = "0";
    /// <summary />
    public const string ClusterSuffix = "Cluster";
    /// <summary />
    public const string ApiPathPrefix = "/api";
    /// <summary />
    public const string RoutePattern = "/{**catch-all}";
    /// <summary />
    public const string DaprServiceInvocation = "/v1.0/invoke/{0}/method{1}";
    /// <summary />
    public static readonly DaprApp WebAdmin = new(AppId: "admin", DaprHttpPort: 23301, HttpPort: 24401,  BasePath: "/admin");
    /// <summary />
    public static readonly DaprApp AuthApi = new(AppId: "auth-api", DaprHttpPort: 23302, HttpPort: 24402, BasePath: "/api/auth", Order: 95);
    /// <summary />
    public static readonly DaprApp AuthAdmin = new(AppId: "auth-admin", DaprHttpPort: 23303, HttpPort: 24403, BasePath: "/auth/admin", Order: 90);  
    /// <summary />
    public static readonly DaprApp AuthSts = new(AppId: "auth-sts", DaprHttpPort: 23304, HttpPort: 24404, BasePath: "/auth");
    /// <summary />
    public static readonly DaprApp Ordering = new(AppId: "order", DaprHttpPort: 23305, HttpPort: 24405);
    /// <summary />
    public static readonly DaprApp Identity = new(AppId: "identity", DaprHttpPort: 23306, HttpPort: 24406);
    /// <summary />
    public static readonly DaprApp Catalog = new(AppId: "catalog", DaprHttpPort: 23307, HttpPort: 24407);
    /// <summary />
    public static readonly DaprApp ProxyServer = new(AppId: "proxy-server", DaprHttpPort: 23300, HttpPort: 24400);

    /// <summary>
    /// api 服务应用 ，Yarp 转发到 dapr cli 通过 dapr service invoke 调用  应用服务
    /// </summary>
    public static IEnumerable<DaprApp> ApiApps
    {
        get
        {
            yield return Ordering;
            yield return Identity;
            yield return Catalog;
        }
    }

    /// <summary>
    /// 系统 app ， Yarp 直接转发到应用，不通过 dapr cli 调用 应用服务
    /// </summary>
    public static IEnumerable<DaprApp> SystemApps
    {
        get
        {
            yield return WebAdmin;
            yield return AuthAdmin;
            yield return AuthApi;
            yield return AuthSts;
        }
    }
}
