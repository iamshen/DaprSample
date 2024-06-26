﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace DaprTool.BuildingBlocks.Utils.Reflection;

public static class AssemblyManager
{
    private static readonly string[] Filters =
        { "dotnet-", "Microsoft.", "mscorlib", "netstandard", "System", "Windows" };

    private static Assembly[]? _allAssemblies;
    private static Type[]? _allTypes;

    static AssemblyManager()
    {
        AssemblyFilterFunc = name => { return name.Name != null && !Filters.Any(m => name.Name.StartsWith(m)); };
    }

    /// <summary>
    ///     设置 程序集过滤器
    /// </summary>
    public static Func<AssemblyName, bool> AssemblyFilterFunc { private get; set; }

    /// <summary>
    ///     获取 所有程序集
    /// </summary>
    public static Assembly[] AllAssemblies
    {
        get
        {
            if (_allAssemblies == null) Init();

            return _allAssemblies ?? Array.Empty<Assembly>();
        }
    }

    /// <summary>
    ///     获取 所有类型
    /// </summary>
    public static Type[] AllTypes
    {
        get
        {
            if (_allTypes == null) Init();

            return _allTypes ?? Array.Empty<Type>();
        }
    }

    /// <summary>
    ///     初始化
    /// </summary>
    [RequiresAssemblyFiles("get all assemblyfiles", Url = "http://help/assemblyfiles")]
    public static void Init()
    {
        if (AssemblyFilterFunc == null) throw new Exception("AssemblyManager.AssemblyFilterFunc 不能为空");

        _allAssemblies = DependencyContext.Default?.GetDefaultAssemblyNames()?
            .Where(AssemblyFilterFunc)?.Select(Assembly.Load)?.ToArray();
        _allTypes = _allAssemblies?.SelectMany(m => m.GetTypes())?.ToArray();
    }


    /// <summary>
    ///     查找指定条件的类型
    /// </summary>
    public static Type[] FindTypes(Func<Type, bool> predicate) => AllTypes.Where(predicate).ToArray();

    /// <summary>
    ///     查找指定基类的实现类型
    /// </summary>
    public static Type[] FindTypesByBase<TBaseType>()
    {
        var baseType = typeof(TBaseType);
        return FindTypesByBase(baseType);
    }

    /// <summary>
    ///     查找指定基类的实现类型
    /// </summary>
    public static Type[] FindTypesByBase(Type baseType)
    {
        return AllTypes.Where(type => type.IsDeriveClassFrom(baseType)).Distinct().ToArray();
    }

    /// <summary>
    ///     查找指定Attribute特性的实现类型
    /// </summary>
    public static Type[] FindTypesByAttribute<TAttribute>(bool inherit = true)
    {
        var attributeType = typeof(TAttribute);
        return FindTypesByAttribute(attributeType, inherit);
    }

    /// <summary>
    ///     查找指定Attribute特性的实现类型
    /// </summary>
    public static Type[] FindTypesByAttribute(Type attributeType, bool inherit = true)
    {
        return AllTypes.Where(type => type.IsDefined(attributeType, inherit)).Distinct().ToArray();
    }
}