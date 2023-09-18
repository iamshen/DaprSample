using System.Reflection;
using Autofac;
using DaprTool.BuildingBlocks.EventBus.Abstractions;
using Ordering.Application.DbEventHandler;
using Module = Autofac.Module;

namespace Microsoft.Extensions.DependencyInjection;

public class AutofacEventHandlerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // 注册 事件调度器
        builder.RegisterType<EventDispatcher>().SingleInstance();
        
        // TODO: 升级.net 8 之后 可以使用 KeyedService 直接替换。
        // 自动注册所有实现了 IDbEventHandler 接口的事件处理器 
        var applicationAssembly = Assembly.GetAssembly(typeof(TradeOrderDbEventHandler));
        builder.RegisterAssemblyTypes(applicationAssembly!)
            .Where(t => typeof(IDbEventHandler).IsAssignableFrom(t))
            .AsImplementedInterfaces()
            .Keyed<IDbEventHandler>(t => t.Name) // 使用类名作为键
            .InstancePerLifetimeScope(); // 设置生命周期为 Scope
    }
}