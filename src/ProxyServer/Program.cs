using Serilog.Sinks.SystemConsole.Themes;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

using var serilog = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(serilog, dispose: false);

builder.AddServiceDefaults();

builder.Services.AddAntiforgery();

// 添加 Kestrel 服务器配置，调整响应头大小限制
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxResponseBufferSize = 131072; // 增加到 128KB，根据需要调整
});

// 如果不知道 路由 匹配 match 怎么配置 请参考链接
// https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-templates
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .LoadHardCodeConfig(builder)
    .AddServiceDiscoveryDestinationResolver()
    ;

var app = builder.Build();
app.UseMiddleware<RequestResponseLoggerMiddleware>();

app.UseWebSockets();
app.UseRouting();
app.UseAntiforgery();
app.MapDefaultEndpoints();

app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.UseSessionAffinity();
    proxyPipeline.UseLoadBalancing();
});

app.Run();