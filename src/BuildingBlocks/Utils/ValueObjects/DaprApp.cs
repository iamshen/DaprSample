using DaprTool.BuildingBlocks.Utils.Constant;

namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

public record DaprApp(string AppId, int? DaprHttpPort, int? ResourceHttpPort, int? ResourceHttpsPort, string? BasePath = "", int? Order = 100)
{
    public string? ResourceHttpsEndpoint => string.Concat(AppId, "-https");
    public string? ResourceHttpEndpoint => string.Concat(AppId, "-http");

    public string MatchPath => string.IsNullOrWhiteSpace(BasePath) ?
        $"{ApplicationConstants.ApiPathPrefix}/{AppId}{ApplicationConstants.RoutePattern}" :
        $"{BasePath}{ApplicationConstants.RoutePattern}";

    public string ClusterId => string.Concat(AppId, ApplicationConstants.ClusterSuffix);
}