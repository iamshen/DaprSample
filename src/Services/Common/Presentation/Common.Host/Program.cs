using Common.Domain.Actors;
using NLog;
using NLog.Web;

const string appName = "Common.Host";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("开始初始化 ({ApplicationName})...", appName);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<NumberGeneratorActor>();
});

var app = builder.Build();

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