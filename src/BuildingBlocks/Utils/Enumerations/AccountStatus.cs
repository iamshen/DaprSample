namespace DaprTool.BuildingBlocks.Utils.Enumerations;

#region 账号状态

/// <summary>
/// 账号状态
/// </summary>
public enum AccountStatus
{
    /// <summary>
    ///     正常 - 账户处于活跃状态，可以正常使用。
    /// </summary>
    Normal = 1,

    /// <summary>
    ///     冻结 - 账户由于违反规定或安全原因被暂时冻结。
    /// </summary>
    Freeze,

    /// <summary>
    ///     注销 - 账户已被用户主动注销或因其他原因永久关闭。
    /// </summary>
    Cancelled
}

#endregion