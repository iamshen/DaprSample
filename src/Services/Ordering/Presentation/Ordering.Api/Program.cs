using DaprTool.BuildingBlocks.Domain.Actors;
using DaprTool.BuildingBlocks.Utils.Constant;
using Ordering.Domain.Actors;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.AddServiceDefaults();
// 注册 应用 服务
builder.RegisterAppServices();
// 注册 Dapr 服务
builder.RegisterAppDapr(options =>
{
    options.Actors.RegisterActor<SerialNumberActor>();
    options.Actors.RegisterActor<PurchaseOrderProcessActor>();
});

var app = builder.Build();


app.MapDefaultEndpoints();

app.UseAppServer(builder.Configuration);

app.MapGet("/", () => Results.LocalRedirect("~/docs"));
app.MapControllers();
app.MapSubscribeHandler();
app.MapActorsHandlers();

try
{
    app.Logger.LogInformation("Starting web api ({ApplicationName})...", Constants.Ordering.AppId);
    await app.RunAsync();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Api ({ApplicationName}) 意外终止...", Constants.Ordering.AppId);
}
finally
{
    app.Logger.LogCritical("stop finally web api ({ApplicationName})... ", Constants.Ordering.AppId);
    Log.CloseAndFlush();
}