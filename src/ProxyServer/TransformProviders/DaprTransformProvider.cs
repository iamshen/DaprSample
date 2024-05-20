using DaprTool.BuildingBlocks.Utils.Constant;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public class DaprTransformProvider: ITransformProvider
{
    public void Apply(TransformBuilderContext context)
    {
        var route = Constants.ApiApps.FirstOrDefault(x => x.AppId.Equals(context.Route.RouteId, StringComparison.OrdinalIgnoreCase));
        if (route is not null)
        {
            context.AddRequestTransform((RequestTransformContext transformContext) =>
            {
                string catchAll = string.Empty;
                var requestPath = transformContext.Path.Value!;

                if (string.IsNullOrWhiteSpace(route.BasePath) && requestPath.StartsWith(Constants.ApiPathPrefix))
                {
                    catchAll = requestPath[$"{Constants.ApiPathPrefix}/{route.AppId}".Length..];
                }
                else
                {
                    catchAll = requestPath;
                }

                var queryContext = new QueryTransformContext(transformContext.HttpContext.Request);

                var newPathUri = RequestUtilities.MakeDestinationAddress(
                    transformContext.DestinationPrefix,
                    new PathString(string.Format(Constants.DaprServiceInvocation, context.Route.RouteId, catchAll)),
                    queryContext.QueryString);

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