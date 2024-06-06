using DaprTool.BuildingBlocks.Utils.Constant;

namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

public record DaprApp(string AppId, int? DaprHttpPort, int? HttpPort, string? BasePath = "", int? Order = 100)
{
    public string? ResourceHttpsEndpoint => string.Concat(AppId, "-https");
    public string? ResourceHttpEndpoint => string.Concat(AppId, "-http");

    public string MatchPath => string.IsNullOrWhiteSpace(BasePath) ?
        $"{Constants.ApiPathPrefix}/{AppId}{Constants.RoutePattern}" :
        $"{BasePath}{Constants.RoutePattern}";

    public string ClusterId => string.Concat(AppId, Constants.ClusterSuffix);
}