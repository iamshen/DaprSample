using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DaprTool.BuildingBlocks.Utils;

#region 枚举助手类

/// <summary>
///     枚举助手类
/// </summary>
public class EnumHelper
{
    #region 获取枚举名称列表

    /// <summary>
    ///     获取枚举名称列表
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    /// <returns></returns>
    public static IEnumerable<(int Value, string Code, string Name)> GetEnumNames(Type enumType)
    {
        return GetEnumItems(enumType)
            .Select(m => (m.ToInt32(), m.ToString(), m.GetDisplayName()));
    }

    #endregion

    #region 获取枚举项

    /// <summary>
    ///     获取枚举项
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    /// <returns></returns>
    public static IEnumerable<Enum> GetEnumItems(Type enumType)
    {
        if (!enumType.IsEnum)
            return new List<Enum>();

        var items = from Enum item in Enum.GetValues(enumType) select item;
        return OrderEnumItems(items.ToArray());
    }

    /// <summary>
    ///     获取枚举项
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TEnum> GetEnumItems<TEnum>() where TEnum : struct, Enum
    {
        if (!typeof(TEnum).IsEnum)
            return new List<TEnum>();

        var items = Enum.GetValues<TEnum>();
        return OrderEnumItems(items.ToArray());
    }

    /// <summary>
    ///     对枚举项目排序
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static IEnumerable<TEnum> OrderEnumItems<TEnum>(TEnum[] items)
    {
        if (items is null || !items.Any())
            return new List<TEnum>();

        var enumType = items.FirstOrDefault()!.GetType();
        var newList = (from item in items
            let attribute = enumType.GetField(item.ToString() ?? string.Empty)?.GetCustomAttribute<DisplayAttribute>()
            select (item, attribute is null ? 0 : attribute.GetOrder() ?? 0)).ToList();

        return newList.OrderBy(x => x.Item2).Select(m => m.item);
    }

    #endregion

    #region 获取Flags枚举包含的全部枚举列表

    /// <summary>
    ///     获取Flags枚举包含的全部枚举列表
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<TEnum> GetEnumFalgsItems<TEnum>(TEnum enumValue) where TEnum : struct, Enum
    {
        var value = Convert.ToInt32(enumValue);
        List<TEnum> enums = new();

        foreach (var item in GetEnumItems<TEnum>())
        {
            var itemValue = Convert.ToInt32(item);
            if (itemValue == 0) //枚举值为0,不处理
                continue;

            if ((itemValue & value) == itemValue)
                enums.Add(item);
        }

        return enums;
    }

    /// <summary>
    ///     获取Flags枚举包含的全部枚举列表
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static IEnumerable<Enum> GetEnumFalgsItems(object enumValue)
    {
        var value = Convert.ToInt32(enumValue);
        List<Enum> enums = new();

        foreach (var item in GetEnumItems(enumValue.GetType()))
        {
            var itemValue = Convert.ToInt32(item);
            if (itemValue == 0) //枚举值为0,不处理
                continue;

            if ((itemValue & value) == itemValue)
                enums.Add(item);
        }

        return enums;
    }

    #endregion

    #region 判断枚举是否包含多个值,Flags标识的枚举可能包含多个值

    /// <summary>
    ///     判断枚举是否包含多个值,Flags标识的枚举可能包含多个值
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static bool IsEnumSingleValue<TEnum>(TEnum enumValue) where TEnum : struct, Enum
    {
        return GetEnumFalgsItems(enumValue).Count() == 1;
    }

    /// <summary>
    ///     判断枚举是否包含多个值,Flags标识的枚举可能包含多个值
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static bool IsEnumSingleValue(object enumValue)
    {
        return GetEnumFalgsItems(enumValue).Count() == 1;
    }

    #endregion
}

#endregion