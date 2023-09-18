using Dapr.Client;
using Dapr.Extensions.Configuration;
using DaprTool.BuildingBlocks.CommonUtility.Constant;
using DaprTool.BuildingBlocks.EventBus;
using DaprTool.BuildingBlocks.EventBus.Abstractions;
using HealthChecks.UI.Client;
using NLog;
using NLog.Web;
using Ordering.Infrastructure.Shared.Options;

const string appName = "Ordering.Api";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("开始初始化 API 服务 ({ApplicationName})...", appName);

var builder = WebApplication.CreateBuilder(args);

var client = new DaprClientBuilder().Build();
// 注册 Dapr secret 配置
builder.Configuration.AddDaprSecretStore(DaprConstants.SecretStore, client);
// 注册 Dapr 配置
builder.Configuration.AddDaprConfigurationStore(DaprConstants.ConfigurationStore, Array.Empty<string>(), client, TimeSpan.FromMinutes(30));
// 注册 订单 配置
builder.Services.Configure<OrderingSettings>(builder.Configuration);
// 注册 Dapr 事件总线
builder.Services.AddScoped<IEventBus, DaprEventBus>();
// 注册 Dapr 客户端
builder.Services.AddDaprClient();
// 注册 Api 控制器
builder.Services.AddControllers();
// 注册 健康检查
const string dbConnectionStringName = "OrderingDB";
builder.AddAppHealthChecks("ordering-db-check", dbConnectionStringName);
// 注册 业务数据库
builder.Services.AddAppDataConnection(builder.Configuration.GetConnectionString(dbConnectionStringName)!);
// 注册 Swagger 
builder.AddAppSwagger();
// 注册 Api 资源
builder.AddAppApiResource();
// 注册 Autofac
builder.AddAppAutofac(); 
// 注册 Nlog
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseAppSwagger(builder.Configuration);
}

app.UseCloudEvents();

app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
app.MapControllers();
app.MapSubscribeHandler();

app.MapLivenessHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);

try   
{
    app.Logger.LogInformation("Starting web api ({ApplicationName})...", appName);
    app.Logger.LogInformation("CounterPrefix: {CounterPrefix}", builder.Configuration["CounterPrefix"]);
    app.Logger.LogInformation("CounterQuantity: {CounterQuantity}", builder.Configuration["CounterQuantity"]);
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