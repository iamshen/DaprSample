namespace MultiTenancy;

/// <summary>
///     租户接口
/// </summary>
public interface IMultiTenant
{
    /// <summary>租户的 ID</summary>
    Guid? TenantId { get; }
}