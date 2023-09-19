using Dapr.Client;
using Dapr.Extensions.Configuration;
using DaprTool.BuildingBlocks.CommonUtility.Constant;
using Ordering.Domain.Actors;
using Serilog;

const string appName = "Ordering.Host";

var builder = WebApplication.CreateBuilder(args);

// 注册 Serilog
builder.AddAppSerilog(appName);
// 注册 Dapr secret 配置
var client = new DaprClientBuilder().Build();
builder.Configuration.AddDaprSecretStore(DaprConstants.SecretStore, client);
// 注册 Dapr 配置
builder.Configuration.AddDaprConfigurationStore(DaprConstants.ConfigurationStore, Array.Empty<string>(), client,
    TimeSpan.FromMinutes(30));
// 注册 Actor 
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<NumberGeneratorActor>();
    options.Actors.RegisterActor<TradeOrderProcessActor>();
});
builder.AddAppServices();
builder.AddAppValidators();

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
    app.Logger.LogCritical(ex, "Host ({ApplicationName}) 意外终止...", appName);
}
finally
{
    Log.CloseAndFlush();
}