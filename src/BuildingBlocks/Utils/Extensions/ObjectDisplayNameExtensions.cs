using DaprTool.BuildingBlocks.Utils;

namespace System;

#region 对象的获取DisplayAttribute特性的扩展方法

/// <summary>
///     对象的获取DisplayAttribute特性的扩展方法
/// </summary>
public static class ObjectDisplayNameExtensions
{
    #region 获取枚举上的DisplayAttribute特性的Name属性

    /// <summary>
    ///     获取枚举上的DisplayAttribute特性的Name属性
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum @enum, bool isCulture = true, bool inherit = false)
    {
        return AttributeHelper.GetDisplayName(@enum, isCulture, inherit);
    }

    /// <summary>
    ///     获取Flags标识的枚举值所包含的全部枚举的DisplayAttribute特性的Name属性
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enum"></param>
    /// <param name="isCulture"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetDisplayNames<TEnum>(this TEnum @enum, bool isCulture = true,
        bool inherit = false) where TEnum : struct, Enum
    {
        return EnumHelper.GetEnumFalgsItems(@enum).Select(m => m.GetDisplayName(isCulture, inherit));
    }

    #endregion

    #region 获取枚举上的DisplayAttribute特性的ShortName属性

    /// <summary>
    ///     获取枚举的DisplayAttribute的ShortName
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayShortName(this Enum @enum, bool isCulture = true, bool inherit = false)
    {
        return AttributeHelper.GetDisplayShortName(@enum, isCulture, inherit);
    }

    /// <summary>
    ///     获取Flags标识的枚举值所包含的全部枚举的DisplayAttribute特性的ShortName属性
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetDisplayShortNames<TEnum>(this TEnum @enum, bool isCulture = true,
        bool inherit = false) where TEnum : struct, Enum
    {
        return EnumHelper.GetEnumFalgsItems(@enum).Select(m => m.GetDisplayShortName(isCulture, inherit));
    }

    #endregion

    #region 获取枚举上的DisplayAttribute特性的Description属性

    /// <summary>
    ///     获取枚举上的DisplayAttribute特性的Description属性
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static string GetDisplayDescription(this Enum @enum, bool isCulture = true, bool inherit = false)
    {
        return AttributeHelper.GetDisplayDescription(@enum, isCulture, inherit);
    }

    /// <summary>
    ///     获取Flags标识的枚举值所包含的全部枚举的DisplayAttribute特性的Description属性
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetDisplayDescriptions<TEnum>(this TEnum @enum, bool isCulture = true,
        bool inherit = false) where TEnum : struct, Enum
    {
        return EnumHelper.GetEnumFalgsItems(@enum).Select(m => m.GetDisplayDescription(isCulture, inherit));
    }

    #endregion
}

#endregion