
using Aspire.Hosting.Dapr;
using System.Collections.Immutable;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using DaprTool.BuildingBlocks.Utils.Constant;

public static class DaprAppExtensions
{
    private static int? DaprHttpMaxRequestSize = 60;
    private static int? DaprHttpReadBufferSize = 128;

    public static DaprSidecarOptions GetSideCarOptions(this DaprApp app) => new DaprSidecarOptions
    {
        AppId = app.AppId,
        DaprHttpPort = app.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(Constants.ResourcesPath),
        DaprHttpMaxRequestSize = DaprHttpMaxRequestSize,
        DaprHttpReadBufferSize = DaprHttpReadBufferSize,
    };
}
