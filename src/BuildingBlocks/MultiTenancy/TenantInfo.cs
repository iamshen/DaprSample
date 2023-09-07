namespace MultiTenancy;

/// <summary>
///     租户信息
/// </summary>
public class TenantInfo
{
    public TenantInfo(Guid? tenantId, string? name = null)
    {
        TenantId = tenantId;
        Name = name;
    }

    /// <summary>
    ///     租户的 ID
    /// </summary>
    public Guid? TenantId { get; }

    /// <summary>
    ///     租户名称
    /// </summary>
    public string? Name { get; }
}