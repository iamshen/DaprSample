using DaprTool.BuildingBlocks.Domain.Actors;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc;
using Ordering.Domain.Actors;

const string appName = "Ordering.Api";

var builder = WebApplication.CreateBuilder(args);

// 注册 应用服务
builder.AddAppServices();

// 注册 Dapr
builder.AddAppDapr(options =>
{
    options.Actors.RegisterActor<NumberGeneratorActor>();
    options.Actors.RegisterActor<PurchaseOrderProcessActor>();
});



var app = builder.Build();

app.UseGlobalException();
app.UseLanguage();
app.UseTraceInfo();
app.UseAppSwagger(builder.Configuration);
app.UseCloudEvents();


app.MapGet("/", () => Results.LocalRedirect("~/docs"));
app.MapControllers();
app.MapSubscribeHandler();
app.MapActorsHandlers();
app.MapLivenessHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);

try
{
    app.Logger.LogInformation("Starting web api ({ApplicationName})...", appName);
    await app.RunAsync();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Api ({ApplicationName}) 意外终止...", appName);
}
finally
{
    app.Logger.LogCritical("stop finally web api ({ApplicationName})... ", appName);
    Serilog.Log.CloseAndFlush();
}