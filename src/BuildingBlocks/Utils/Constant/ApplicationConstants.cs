using DaprTool.BuildingBlocks.Utils.ValueObjects;

namespace DaprTool.BuildingBlocks.Utils.Constant;

/// <summary />
public static class ApplicationConstants
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
    public static string DefaultActorId = "0";
    /// <summary />
    public const string ClusterSuffix = "Cluster";
    /// <summary />
    public const string ApiPathPrefix = "/api";
    /// <summary />
    public const string RoutePattern = "/{**catch-all}";
    /// <summary />
    public const string DaprServiceInvocation = "/v1.0/invoke/{0}/method{1}";
    /// <summary />
    public static DaprApp ProxyServer = new("proxy-server", 12001, 44440, 44444);
    /// <summary />
    public static DaprApp WebAdmin = new("admin", 12010, 51871, 51873, "/admin");
    /// <summary />
    public static DaprApp AuthAdmin = new("auth-admin", 12030, 53871, 53873, "/auth/admin", 90);
    /// <summary />
    public static DaprApp AuthApi = new("auth-api", 12040, 54871, 54873, "/api/auth", 95);
    /// <summary />
    public static DaprApp AuthSts = new("auth-sts", 12020, 52871, 52873, "/auth");
    /// <summary />
    public static DaprApp Ordering = new("order-api", 12050, 31441, 31442);
    /// <summary />
    public static DaprApp Identity = new("identity-api", 12060, 32441, 32442);
    /// <summary />
    public static DaprApp Catalog = new("catalog-api", 12070, 33441, 33442);

    /// <summary />
    public static IEnumerable<DaprApp> AllRoutes
    {
        get
        {
            //yield return ProxyServer;// 
            //yield return WebAdmin;
            yield return AuthSts;
            yield return AuthAdmin;
            yield return AuthApi;
            yield return Ordering;
            yield return Identity;
            yield return Catalog;
        }
    }
}
