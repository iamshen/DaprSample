using System.Runtime.CompilerServices;
using DaprTool.BuildingBlocks.Utils;

namespace System;

#region 枚举的相关扩展方法

/// <summary>
///     枚举的相关扩展方法
/// </summary>
public static class EnumerationExtensions
{
    #region 获取枚举的全部的名称、值等

    /// <summary>
    ///     获取枚举的全部的名称、值等
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static IEnumerable<(int Value, string Code, string Name)> GetEnumNames(this Enum @enum)
    {
        return EnumHelper.GetEnumNames(@enum.GetType());
    }

    #endregion

    #region 获取Falgs枚举包含的全部枚举

    /// <summary>
    ///     获取Falgs枚举包含的全部枚举
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static IEnumerable<TEnum> GetEnumFalgsItems<TEnum>(this TEnum obj) where TEnum : struct, Enum
    {
        return EnumHelper.GetEnumFalgsItems(obj);
    }

    #endregion

    #region 判断枚举是否包含多个值,Flags标识的枚举可能包含多个值

    /// <summary>
    ///     判断枚举是否包含多个值,Flags标识的枚举可能包含多个值
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsEnumSingleValue<TEnum>(this TEnum obj) where TEnum : struct, Enum
    {
        return obj.GetEnumFalgsItems().Count() == 1;
    }

    #endregion

    #region 获取枚举的值

    /// <summary>
    ///     获取枚举的值
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte ToByte(this Enum @enum)
    {
        return (byte)@enum.GetHashCode();
    }

    /// <summary>
    ///     获取枚举的值
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short ToShort(this Enum @enum)
    {
        return (short)@enum.GetHashCode();
    }

    /// <summary>
    ///     获取枚举的值
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32(this Enum @enum)
    {
        return @enum.GetHashCode();
    }

    /// <summary>
    ///     获取枚举的值
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64(this Enum @enum)
    {
        return @enum.GetHashCode();
    }

    #endregion

    #region 获取枚举的全部项

    /// <summary>
    ///     获取枚举的全部项
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static IEnumerable<Enum> GetEnumItems(this Enum @enum)
    {
        return EnumHelper.GetEnumItems(@enum.GetType());
    }

    /// <summary>
    ///     获取枚举的全部项
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static IEnumerable<TEnum> GetEnumItems<TEnum>(this TEnum @enum) where TEnum : struct, Enum
    {
        return EnumHelper.GetEnumItems<TEnum>();
    }

    #endregion
}

#endregion