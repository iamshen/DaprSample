using LinqToDB;
using LinqToDB.Mapping;

namespace DaprTool.BuildingBlocks.Linq2DbAccessor.Data;

/// <summary>
///     实体类基类
/// </summary>
public abstract class EntityBase : IEntity, ICreatedTime, IUpdatedTime, ISoftDeleted
{
    /// <summary>
    ///     Id 主键
    /// </summary>
    [Column("id", Order = 1, IsPrimaryKey = true, CanBeNull = false)]
    public virtual string Id { get; set; } = string.Empty;

    /// <summary>
    ///     创建时间
    /// </summary>
    [Column("created_time", Order = -50, DataType = DataType.Int64, CanBeNull = false)]
    public virtual long CreatedTime { get; set; } = TimeProvider.System.GetTimestamp();

    /// <summary>
    ///     上一次更新时间
    /// </summary>
    [Column("updated_time", Order = -45, DataType = DataType.Int64, CanBeNull = false)]
    public virtual long UpdatedTime { get; set; } = TimeProvider.System.GetTimestamp();

    /// <summary>
    ///     删除时间(软删除)
    /// </summary>
    /// <remarks>如果 删除时间 > 0 说明 数据被删除了</remarks>
    [Column("deleted_time", Order = -40, DataType = DataType.Int64, CanBeNull = false)]
    public virtual long DeletedTime { get; set; }
}