using DaprTool.BuildingBlocks.Domain.Actors;
using HealthChecks.UI.Client;
using Ordering.Domain.Actors;

var builder = WebApplication.CreateBuilder(args);

// 注册 应用 服务
builder.RegisterAppServices();
// 注册 Dapr 服务
builder.RegisterAppDapr(options =>
{
    options.Actors.RegisterActor<SerialNumberActor>();
    options.Actors.RegisterActor<PurchaseOrderProcessActor>();
});

var app = builder.Build();

app.UseAppServer(builder.Configuration);

app.MapGet("/", () => Results.LocalRedirect("~/docs"));
app.MapControllers();
app.MapSubscribeHandler();
app.MapActorsHandlers();
app.MapLivenessHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);

await app.RunAsync();
