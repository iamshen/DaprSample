using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     注册 Autofac
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppAutofac(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>((_, containerBuilder) =>
        {
            containerBuilder.RegisterModule(new AutofacAppServiceModule());
            containerBuilder.RegisterModule(new AutofacEventHandlerModule());
        });
    }
}