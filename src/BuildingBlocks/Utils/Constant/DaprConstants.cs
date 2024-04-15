using DaprTool.BuildingBlocks.Utils.ValueObjects;

namespace DaprTool.BuildingBlocks.Utils.Constant;

/// <summary>
///  Dapr Constants
/// </summary>
public static class DaprConstants
{
    // components
    public const string PubSubName = "dt-pubsub";
    public const string SecretStore = "dt-secretstore";
    public const string ConfigurationStore = "dt-configurationstore";

    // default actorId
    public static string DefaultActorId = "0";

    // dapr apps
    public static DaprApp Ordering = new("Ordering");
    public static DaprApp Identity = new("Identity");
    public static DaprApp Catalog = new("Catalog");
}