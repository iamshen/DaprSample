using HealthChecks.UI.Client;

const string appName = "Ordering.Api";

var builder = WebApplication.CreateBuilder(args);

// 注册 Dapr
builder.AddAppDapr();
// 注册 应用服务
builder.AddAppServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseAppSwagger(builder.Configuration);
}

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