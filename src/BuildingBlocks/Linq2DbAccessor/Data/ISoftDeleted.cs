namespace DaprTool.BuildingBlocks.Linq2DbAccessor.Data;

public interface ISoftDeleted
{
    /// <summary>
    ///     是否已删除
    /// </summary>
    public long DeletedTime { get; set; }
}