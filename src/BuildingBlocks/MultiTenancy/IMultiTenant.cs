namespace MultiTenancy;

/// <summary>
/// </summary>
public interface IMultiTenant
{
    /// <summary>租户的 ID</summary>
    Guid? TenantId { get; }
}