#nullable disable

namespace DaprTool.BuildingBlocks.CommonUtility.ValueObjects;

#region 值对象

/// <summary>
///     值对象
/// </summary>
public class ValueItem
{
    #region 将Value转为整形

    /// <summary>
    ///     将Value转为整形
    /// </summary>
    /// <returns></returns>
    public int ToInt32()
    {
        return int.Parse(Value?.ToString() ?? string.Empty);
    }

    #endregion

    #region 将Value转为字符串

    /// <summary>
    ///     将Value转为字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Value.ToString();
    }

    #endregion

    #region 属性

    /// <summary>
    ///     配置值
    /// </summary>
    public object Value { get; init; }

    /// <summary>
    ///     是否隐私数据
    /// </summary>
    public bool Privacy { get; init; }

    /// <summary>
    ///     备注
    /// </summary>
    public string Remark { get; init; }

    #endregion

    #region 初始化

    /// <summary>
    ///     支付服务配置对象
    /// </summary>
    private ValueItem()
    {
    }

    /// <summary>
    ///     支付服务配置对象
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="privacy">是否隐私数据</param>
    /// <param name="remark">备注</param>
    public ValueItem(object value, bool privacy = false, string remark = null)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Remark = remark;
        Privacy = privacy;
    }

    #endregion
}

#endregion