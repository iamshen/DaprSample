using DaprTool.BuildingBlocks.EventBus;
using DaprTool.BuildingBlocks.EventBus.Abstractions;
using FluentValidation;
using Ordering.Domain.Core.Commands.TradeOrder;
using Ordering.Infrastructure.Shared.Options;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppValidators(this WebApplicationBuilder builder)
    {
        // Add  Validators 
        builder.Services.AddValidatorsFromAssemblyContaining<CreateTradeOrderCommandValidator>(ServiceLifetime.Transient);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        // 注册 Dapr 客户端
        builder.Services.AddDaprClient();
        // 注册 Dapr 事件总线
        builder.Services.AddScoped<IEventBus, DaprEventBus>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.Configure<OrderingSettings>(builder.Configuration);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="appName"></param>
    public static void AddAppSerilog(this WebApplicationBuilder builder, string appName)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"];

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl!)
            .Enrich.WithProperty("ApplicationName", appName)
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}