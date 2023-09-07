using LinqToDB.Common;

namespace DaprTool.BuildingBlocks.Linq2DbAccessor.Converter;

#region TimeSpan和整形的转换器

/// <summary>
///     TimeSpan和整形的转换器
/// </summary>
public class TimeSpanConverter : ValueConverter<TimeSpan, long>
{
    #region 初始化

    /// <summary>
    ///     TimeSpan和整形的转换器
    /// </summary>
    public TimeSpanConverter() : base(v => v.Ticks, v => TimeSpan.FromTicks(v), true)
    {
    }

    #endregion
}

#endregion