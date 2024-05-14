using DaprTool.BuildingBlocks.Utils.Constant;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

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
                { "destination1", new DestinationConfig() { Address = string.Concat("http://localhost:", ApplicationConstants.WebAdmin.ResourceHttpPort) } },
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

public class DaprTransformProvider(ILogger<DaprTransformProvider> logger) : ITransformProvider
{

    public void Apply(TransformBuilderContext context)
    {
        var route = ApplicationConstants.AllRoutes.FirstOrDefault(x => x.AppId.Equals(context.Route.RouteId, StringComparison.OrdinalIgnoreCase));
        if (route is not null)
        {
            context.AddRequestTransform((RequestTransformContext transformContext) =>
            {
                string catchAll = string.Empty;
                var requestPath = transformContext.Path.Value!;

                if (string.IsNullOrWhiteSpace(route.BasePath) && requestPath.StartsWith(ApplicationConstants.ApiPathPrefix))
                {
                    catchAll = requestPath[$"{ApplicationConstants.ApiPathPrefix}/{route.AppId}".Length..];
                }
                else
                {
                    catchAll = requestPath;
                }

                var queryContext = new QueryTransformContext(transformContext.HttpContext.Request);

                var newPathUri = RequestUtilities.MakeDestinationAddress(
                    transformContext.DestinationPrefix,
                    new PathString(string.Format(ApplicationConstants.DaprServiceInvocation, context.Route.RouteId, catchAll)),
                    queryContext.QueryString);

                logger.LogInformation("proxy to new path uri: {0}", newPathUri);

                transformContext.ProxyRequest.RequestUri = newPathUri;

                return ValueTask.CompletedTask;
            });
        }
    }

    public void ValidateCluster(TransformClusterValidationContext context)
    {
        /*if (context.Cluster.ClusterId== DaprConfigUtils.DaprApiClusterId)
        {
        }*/
    }

    public void ValidateRoute(TransformRouteValidationContext context)
    {
        /*if (context.Route.RouteId == DaprConfigUtils.DaprApiRouteId)
        {
        }*/
    }
}