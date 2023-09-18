using Autofac;
using Ordering.Application.Interfaces;
using Ordering.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;

public class AutofacAppServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TradeOrderService>()
            .As<ITradeOrderService>().InstancePerLifetimeScope();
    }
}