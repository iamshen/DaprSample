using DaprTool.BuildingBlocks.Utils.Constant;
using Yarp.ReverseProxy.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

internal static class ReverseProxyServiceCollectionExtensions
{
    internal static IReadOnlyList<IReadOnlyDictionary<string, string>> DefaultTransform { get; set; } =
    [
        new Dictionary<string, string>
        {
            {"RequestHeader", "X-Forwarded-Host"},
            {"Set", "{Host}"}
        },
        new Dictionary<string, string>
        {
            {"RequestHeader", "X-Forwarded-Prefix"},
            {"Set", ApplicationConstants.WebAdmin.BasePath ?? ""}
        },
        new Dictionary<string, string>
        {
            {"RequestHeaderOriginalHost", "true" },
        },
        new Dictionary<string, string>
        {
            { "ResponseHeadersCopy", "true" }
        }
    ];

    /// <summary>
    /// 添加 常量配置
    /// <para> 把 /api/{appid}/{*remainder} 路由转换为 /v1.0/invoke/{appid}/method/{remainder} </para>
    /// </summary>
    /// <param name="reverseProxyBuilder"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal static IReverseProxyBuilder LoadHardCodeConfig(this IReverseProxyBuilder reverseProxyBuilder, WebApplicationBuilder builder)
    {
        var routes = Routes.Concat(SystemRoutes).ToArray();
        var clusters = Clusters.Concat(SystemClusters).ToArray();

        return reverseProxyBuilder.LoadFromMemory(routes, clusters).AddTransforms<DaprTransformProvider>();
    }

    /// <summary /> 
    internal static IEnumerable<RouteConfig> SystemRoutes =>
    [
        new RouteConfig()
        {
            RouteId = ApplicationConstants.WebAdmin.AppId,
            ClusterId = ApplicationConstants.WebAdmin.ClusterId,
            Order = ApplicationConstants.WebAdmin.Order,
            Match = new RouteMatch()
            {
                Path = ApplicationConstants.WebAdmin.MatchPath,
            },
            Transforms = DefaultTransform
        }
    ];

    /// <summary /> 
    internal static IEnumerable<ClusterConfig> SystemClusters =>
    [
        new ClusterConfig()
        {
            ClusterId = ApplicationConstants.WebAdmin.ClusterId,
            Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
            {
                { "destination1", new DestinationConfig() { Address = string.Concat("http://", ApplicationConstants.WebAdmin.AppId) } },
                { "destination2", new DestinationConfig() { Address = string.Concat("http://localhost:", ApplicationConstants.WebAdmin.ResourceHttpPort) } },
            }
        }
    ];

    /// <summary /> 
    internal static IEnumerable<RouteConfig> Routes => ApplicationConstants.AllRoutes.Select(x => new RouteConfig()
    {
        RouteId = x.AppId,
        ClusterId = x.ClusterId,
        Order = x.Order,
        Match = new RouteMatch()
        {
            Path = x.MatchPath,
        },
        Transforms = DefaultTransform
    });

    /// <summary /> 
    internal static IEnumerable<ClusterConfig> Clusters => ApplicationConstants.AllRoutes.Select(x => new ClusterConfig()
    {
        ClusterId = x.ClusterId,
        Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
        {
            { "destination1", new DestinationConfig() { Address = string.Concat("http://localhost:", x.DaprHttpPort) } },
        }
    });

}
