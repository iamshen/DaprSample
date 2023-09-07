using LinqToDB;
using LinqToDB.Mapping;

namespace DaprTool.BuildingBlocks.Linq2DbAccessor;

/// <summary>
///     实体类基类
/// </summary>
public abstract class EntityBase : IEntity, ICreatedTime, IUpdatedTime, ISoftDeleted
{
    /// <summary>
    ///     Id 主键
    /// </summary>
    [Column("id", Order = 0, IsPrimaryKey = true, CanBeNull = false)]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    ///     创建时间
    /// </summary>
    [Column("created_time", DataType = DataType.DateTimeOffset, CanBeNull = false)]
    public long CreatedTime { get; set; }

  
    /// <summary>
    ///     删除时间
    /// </summary>
    /// <remarks>删除时间 > 0 说明 数据被删除了</remarks>
    [Column("is_soft_deleted", DataType = DataType.Boolean, CanBeNull = false)]
    public long DeletedTime { get; set; }

    /// <summary>
    ///     上一次更新时间
    /// </summary>
    [Column("updated_time", DataType = DataType.DateTimeOffset, CanBeNull = false)]
    public long UpdatedTime { get; set; }
}