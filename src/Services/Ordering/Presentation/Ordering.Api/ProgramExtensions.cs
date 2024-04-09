using DaprTool.BuildingBlocks.Utils.Constant;
using Ordering.Domain.Observer.PurchaseOrder;
using Ordering.Infrastructure.Shared.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramExtensions
{
    /// <summary>
    ///     添加应用服务
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        // 注册 api 应用服务
        builder.AddAppServices(DaprConstants.Ordering.AppId);
        // 注册 ordering-api 服务
        builder.Services.AddAppApiServices();
        // 注册 MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(PurchaseOrderDbEventHandler).Assembly));
        // 注册 订单 配置
        builder.Services.Configure<OrderingSettings>(builder.Configuration);
        // 注册 应用命令验证器
        builder.AddAppValidators();
        // 注册 业务数据库
        builder.Services.AddAppDataConnection(builder.Configuration.GetConnectionString(DaprConstants.Ordering.AppId)!);
    }
}