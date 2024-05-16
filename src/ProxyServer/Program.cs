var builder = WebApplication.CreateBuilder(args);

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
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
app.MapDefaultEndpoints();

app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.UseSessionAffinity();
    proxyPipeline.UseLoadBalancing();
});

app.Run();