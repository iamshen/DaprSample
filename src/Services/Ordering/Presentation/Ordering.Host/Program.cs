using Dapr.Client;
using Dapr.Extensions.Configuration;
using DaprTool.BuildingBlocks.CommonUtility.Constant;
using NLog;
using NLog.Web;
using Ordering.Domain.Actors;

const string appName = "Ordering.Host";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("开始初始化 ({ApplicationName})...", appName);

var builder = WebApplication.CreateBuilder(args);

// 注册 Dapr secret 配置
var client = new DaprClientBuilder().Build();
builder.Configuration.AddDaprSecretStore(DaprConstants.SecretStore, client);
// 注册 Dapr 配置
builder.Configuration.AddDaprConfigurationStore(DaprConstants.ConfigurationStore, Array.Empty<string>(), client, TimeSpan.FromMinutes(30));
// 注册 Actor 
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<NumberGeneratorActor>();
    options.Actors.RegisterActor<TradeOrderProcessActor>();
});
builder.AddAppServices();
builder.AddAppValidators();
// 注册 Nlog
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// 映射 Actor 相关的Endpoints
app.MapActorsHandlers();

try
{
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Host ({ApplicationName}) 意外终止...", appName);
    app.Logger.LogCritical(ex, "Host ({ApplicationName}) 意外终止...", appName);
}
finally
{
    LogManager.Shutdown();
}