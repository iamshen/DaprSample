using HealthChecks.UI.Client;
using NLog;
using NLog.Web;

const string appName = "Common.Api";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("开始初始化 API 服务 ({ApplicationName})...", appName);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprClient();
builder.Services.AddControllers();
builder.AddAppHealthChecks("common-db-check");
builder.AddAppSwagger("common_api");
builder.AddAppApiResource();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseAppSwagger("common_api");
}

app.UseCloudEvents();

app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
app.MapControllers();
app.MapActorsHandlers();
app.MapSubscribeHandler();

app.MapLivenessHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);

try   

{
    app.Logger.LogInformation("Starting web api ({ApplicationName})...", appName);
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Api ({ApplicationName}) 意外终止 ...", appName);
    app.Logger.LogCritical(ex, "Api ({ApplicationName}) 意外终止...", appName);
}
finally
{
    LogManager.Shutdown();
}