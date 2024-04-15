using DaprTool.BuildingBlocks.Utils.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationExtensions
{
    /// <summary>
    ///     注册 Mediator
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAppMediators(this IServiceCollection services)
    {
        // 注册 MediatR
        services.AddMediatR(c => c.RegisterServicesFromAssemblies(AssemblyManager.AllAssemblies));
        return services;
    }
}