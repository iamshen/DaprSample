var builder = WebApplication.CreateBuilder(args);


// 如果不知道 路由 匹配 match 怎么配置 请参考链接
// https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/routing?view=aspnetcore-8.0#route-templates
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();