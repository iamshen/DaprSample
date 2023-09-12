using HealthChecks.UI.Client;
using NLog;
using NLog.Web;
using Ordering.Infrastructure.Shared.Options;

const string appName = "Ordering.Api";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("开始初始化 API 服务 ({ApplicationName})...", appName);

var builder = WebApplication.CreateBuilder(args);

// 注册 订单 配置
builder.Services.Configure<OrderingSettings>(builder.Configuration);
// 注册 dapr 客户端
builder.Services.AddDaprClient();
// 注册 api 控制器
builder.Services.AddControllers();
// 注册 健康检查
const string dbConnectionStringName = "OrderingDB";
builder.AddAppHealthChecks("ordering-db-check", dbConnectionStringName);
// 注册 业务数据库
builder.Services.AddAppDataConnection(builder.Configuration.GetConnectionString(dbConnectionStringName)!);
// 注册 swagger 
builder.AddAppSwagger("ordering_api");
// 注册 Api 资源
builder.AddAppApiResource();
// 注册 事件处理器
builder.AddEventHandlerAutofac(); 

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