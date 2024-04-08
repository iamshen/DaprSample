using System.ComponentModel;
using System.Reflection;
using DaprTool.BuildingBlocks.Utils;

namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

#region 枚举对象

/// <summary>
///     枚举对象
/// </summary>
public class EnumSubject
{
    #region 属性

    /// <summary>
    ///     名称
    /// </summary>
    public virtual string Name { get; set; } = null!;

    /// <summary>
    ///     标准化名称
    /// </summary>
    public virtual string NormalizedName { get; set; } = null!;

    /// <summary>
    ///     说明
    /// </summary>
    public virtual string? Remark { get; set; }

    /// <summary>
    ///     枚举值、说明
    /// </summary>
    public virtual List<EnumItem> EnumItems { get; init; }

    #endregion

    #region 初始化

    /// <summary>
    ///     枚举对象
    /// </summary>
    /// <param name="enumType"></param>
    public EnumSubject(Type enumType) : this(enumType, null)
    {
    }

    /// <summary>
    ///     枚举对象
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="remark"></param>
    public EnumSubject(Type enumType, string? remark) : this(enumType, null, remark)
    {
    }

    /// <summary>
    ///     枚举对象
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="name"></param>
    /// <param name="remark"></param>
    public EnumSubject(Type enumType, string? name, string? remark)
    {
        if (enumType is null)
            throw new ArgumentNullException(nameof(enumType));

        var remarkAttribute = enumType.GetCustomAttribute<DescriptionAttribute>();
        EnumItems = EnumHelper.GetEnumItems(enumType)
            .Select(m => new EnumItem(m.GetDisplayName(), m.ToString(), m.ToInt32())).ToList();
        Name = name ?? enumType.Name;
        NormalizedName = Name.ToUpperInvariant();
        Remark = remark ?? remarkAttribute?.Description;
    }

    #endregion

    #region 方法

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <param name="enumType"></param>
    /// <returns></returns>
    public static EnumSubject Create(Type enumType)
    {
        return new EnumSubject(enumType);
    }

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static EnumSubject Create<T>() where T : Enum
    {
        return new EnumSubject(typeof(T));
    }

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    public static EnumSubject Create(Type enumType, string? remark)
    {
        return new EnumSubject(enumType, remark);
    }

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="remark"></param>
    /// <returns></returns>
    public static EnumSubject Create<T>(string? remark) where T : Enum
    {
        return new EnumSubject(typeof(T), remark);
    }

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="name"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    public static EnumSubject Create(Type enumType, string? name, string? remark)
    {
        return new EnumSubject(enumType, name, remark);
    }

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    public static EnumSubject Create<T>(string? name, string? remark) where T : Enum
    {
        return new EnumSubject(typeof(T), name, remark);
    }

    #endregion
}

/// <summary>
///     枚举对象
/// </summary>
public class EnumSubject<T> : EnumSubject where T : Enum
{
    #region 初始化

    /// <summary>
    ///     枚举对象
    /// </summary>
    public EnumSubject() : this(null)
    {
    }

    /// <summary>
    ///     枚举对象
    /// </summary>
    /// <param name="remark"></param>
    public EnumSubject(string? remark) : this(null, remark)
    {
    }

    /// <summary>
    ///     枚举对象
    /// </summary>
    public EnumSubject(string? name, string? remark) : base(typeof(T), name, remark)
    {
    }

    #endregion

    #region 方法

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <returns></returns>
    public static EnumSubject<T> Create()
    {
        return new EnumSubject<T>();
    }

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <param name="remark"></param>
    /// <returns></returns>
    public static EnumSubject<T> Create(string? remark)
    {
        return new EnumSubject<T>(remark);
    }

    /// <summary>
    ///     创建枚举对象
    /// </summary>
    /// <param name="name"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    public static EnumSubject<T> Create(string? name, string? remark)
    {
        return new EnumSubject<T>(name, remark);
    }

    #endregion
}

#endregion

#region 枚举项对象

/// <summary>
///     枚举项对象
/// </summary>
public class EnumItem
{
    #region 初始化

    /// <summary>
    ///     枚举项对象
    /// </summary>
    /// <param name="text">枚举显示值</param>
    /// <param name="code">枚举项代码</param>
    /// <param name="value">枚举实际值</param>
    public EnumItem(string text, string code, int value)
    {
        Text = text;
        Code = code;
        Value = value;
    }

    #endregion

    #region 属性

    /// <summary>
    ///     枚举显示值
    /// </summary>
    public string Text { get; } = null!;

    /// <summary>
    ///     枚举代码
    /// </summary>
    public string Code { get; } = null!;

    /// <summary>
    ///     枚举实际值
    /// </summary>
    public int Value { get; }

    #endregion
}

#endregion