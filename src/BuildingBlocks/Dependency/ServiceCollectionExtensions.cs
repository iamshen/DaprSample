using System.Reflection;
using DaprTool.BuildingBlocks.Dependency.Attributes;
using DaprTool.BuildingBlocks.Utils.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DaprTool.BuildingBlocks.Dependency;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     注册自动依赖注入
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoInject(this IServiceCollection services)
    {
        //查找所有自动注册的服务实现类型进行注册
        var dependencyTypes = FindDependencyTypes();
        foreach (var dependencyType in dependencyTypes) AddToServices(services, dependencyType);

        return services;
    }

    #region 私有方法

    #region 将服务实现类型注册到服务集合中

    /// <summary>
    ///     将服务实现类型注册到服务集合中
    /// </summary>
    /// <param name="services"></param>
    /// <param name="implementationType"></param>
    private static void AddToServices(IServiceCollection services, Type implementationType)
    {
        if (implementationType.IsAbstract || implementationType.IsInterface) return;
        var lifetime = GetLifetime(implementationType);
        if (lifetime is null) return;

        var dependencyAttribute = implementationType.GetAttribute<AutoInjectAttribute>();
        var serviceTypes = GetImplementedInterfaces(implementationType);

        //服务数量为0时注册自身
        if (serviceTypes.Length == 0)
            services.TryAdd(new ServiceDescriptor(implementationType, implementationType, lifetime.Value));

        //服务实现显示要求注册身处时，注册自身并且继续注册接口
        if (dependencyAttribute?.AddSelf == true)
            services.TryAdd(new ServiceDescriptor(implementationType, implementationType, lifetime.Value));

        //注册服务
        for (var i = 0; i < serviceTypes.Length; i++)
        {
            var serviceType = serviceTypes[i];
            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime.Value);
            if (lifetime.Value == ServiceLifetime.Transient)
            {
                services.TryAddEnumerable(descriptor);
                continue;
            }

            var multiple = serviceType.HasAttribute<MultipleAutoInjectAttribute>();
            if (multiple)
            {
                services.Add(descriptor);
            }
            else
            {
                if (i != 0)
                {
                    //有多个接口，后边的接口注册使用第一个接口的实例，保证同个实现类的多个接口获得同一实例
                    var firstServiceType = serviceTypes[0];
                    descriptor = new ServiceDescriptor(serviceType, provider => provider.GetService(firstServiceType),
                        lifetime.Value);
                }

                AddSingleService(services, descriptor, dependencyAttribute);
            }
        }
    }

    #endregion

    #region 获取实现类型的所有可注册服务接口

    /// <summary>
    ///     获取实现类型的所有可注册服务接口
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static Type[] GetImplementedInterfaces(Type type)
    {
        Type[] exceptInterfaces = { typeof(IDisposable) };
        var interfaceTypes = type.GetInterfaces()
            .Where(t => !exceptInterfaces.Contains(t) && !t.HasAttribute<IgnoreDependencyAttribute>()).ToArray();
        for (var index = 0; index < interfaceTypes.Length; index++)
        {
            var interfaceType = interfaceTypes[index];
            if (interfaceType.IsGenericType && !interfaceType.IsGenericTypeDefinition && interfaceType.FullName == null)
                interfaceTypes[index] = interfaceType.GetGenericTypeDefinition();
        }

        return interfaceTypes;
    }

    #endregion

    #region 获取要注册的生命周期类型

    /// <summary>
    ///     从类型获取要注册的<see cref="ServiceLifetime" />生命周期类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static ServiceLifetime? GetLifetime(Type type)
    {
        var attribute = type.GetAttribute<AutoInjectAttribute>();
        if (attribute is not null) return attribute.Lifetime;

        if (type.IsDeriveClassFrom<ITransientInject>()) return ServiceLifetime.Transient;

        if (type.IsDeriveClassFrom<IScopeInject>()) return ServiceLifetime.Scoped;

        if (type.IsDeriveClassFrom<ISingletonInject>()) return ServiceLifetime.Singleton;

        return null;
    }

    #endregion

    #region 查找所有自动注册的服务实现类型

    /// <summary>
    ///     查找所有自动注册的服务实现类型
    /// </summary>
    /// <returns></returns>
    private static Type[] FindDependencyTypes()
    {
        Type[] baseTypes = { typeof(ISingletonInject), typeof(IScopeInject), typeof(ITransientInject) };
        return AssemblyManager.FindTypes(type => type.IsClass && type is { IsAbstract: false, IsInterface: false }
                                                              && !type.HasAttribute<IgnoreDependencyAttribute>()
                                                              && (baseTypes.Any(b => b.IsAssignableFrom(type)) ||
                                                                  type.HasAttribute<AutoInjectAttribute>())) ?? [];
    }

    #endregion

    #region 注册单个服务

    /// <summary>
    ///     注册单个服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="descriptor"></param>
    /// <param name="dependencyAttribute"></param>
    private static void AddSingleService(IServiceCollection services,
        ServiceDescriptor descriptor,
        AutoInjectAttribute? dependencyAttribute)
    {
        if (dependencyAttribute?.ReplaceExisting == true)
            services.Replace(descriptor);
        else if (dependencyAttribute?.TryAdd == true)
            services.TryAdd(descriptor);
        else
            services.Add(descriptor);
    }

    #endregion

    #endregion
}