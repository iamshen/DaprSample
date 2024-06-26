﻿using Microsoft.Extensions.DependencyInjection;

namespace DaprTool.BuildingBlocks.Dependency.Attributes;

#region 自动依赖注入

/// <summary>
///     自动依赖注入行为特性
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AutoInjectAttribute : Attribute
{
    /// <summary>
    ///     初始化一个<see cref="AutoInjectAttribute" />类型的新实例
    /// </summary>
    public AutoInjectAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }

    /// <summary>
    ///     获取 生命周期类型，代替
    ///     <see cref="ISingletonInject" />,<see cref="IScopeInject" />,<see cref="ITransientInject" />三个接口的作用
    /// </summary>
    public ServiceLifetime Lifetime { get; }

    /// <summary>
    ///     获取或设置 是否为TryAdd方式注册，通常用于默认服务，当服务可能被替换时，应设置为true
    /// </summary>
    public bool TryAdd { get; set; }

    /// <summary>
    ///     获取或设置 是否替换已存在的服务实现，通常用于主要服务，当服务存在时即优先使用时，应设置为true
    /// </summary>
    public bool ReplaceExisting { get; set; }

    /// <summary>
    ///     获取或设置 是否注册自身类型，默认没有接口的类型会注册自身，当此属性值为true时，也会注册自身
    /// </summary>
    public bool AddSelf { get; set; }
}

#endregion