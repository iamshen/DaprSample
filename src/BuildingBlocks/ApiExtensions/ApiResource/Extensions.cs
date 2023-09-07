#nullable disable

using System.Reflection;

namespace System.Collections.Generic;

/// <summary>
///     类型扩展
/// </summary>
public static class Extensions
{
    /// <summary>
    ///     从类型成员获取指定Attribute特性
    /// </summary>
    /// <typeparam name="T">Attribute特性类型</typeparam>
    /// <param name="memberInfo">类型类型成员</param>
    /// <param name="inherit">是否从继承中查找</param>
    /// <returns>存在返回第一个，不存在返回null</returns>
    public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
    {
        var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
        return attributes.FirstOrDefault() as T;
    }
}

/// <summary>
///     集合扩展方法
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    ///     如果不存在，添加项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <param name="value"></param>
    /// <param name="existFunc"></param>
    public static void AddIfNotExist<T>(this ICollection<T> collection, T value, Func<T, bool> existFunc)
    {
        var exists = existFunc == null ? collection.Contains(value) : collection.Any(existFunc);
        if (!exists) collection.Add(value);
    }
}