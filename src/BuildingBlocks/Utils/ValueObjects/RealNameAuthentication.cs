namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

/// <summary>
///     实名认证资料信息
/// </summary>
public class RealNameAuthentication
{
    public RealNameAuthentication()
    {
        
    }   
    
    /// <summary>
    /// 姓名（真实姓名）
    /// </summary>
    public string FullName { get; set; } = default!;

    /// <summary>
    /// 身份证号码
    /// </summary>
    public string IdentityCardNumber { get; set; } = default!;

    /// <summary>
    /// 性别（0：未知，1：男性，2：女性）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public long DateOfBirth { get; set; }

    /// <summary>
    /// 地址（居住地或身份证上的地址）
    /// </summary>
    public string Address { get; set; } = default!;

    /// <summary>
    /// 认证时间
    /// </summary>
    public long AuthenticationDate { get; set; }

    /// <summary>
    /// 头像照片 Base64 格式（可选）
    /// </summary>
    public string ProfilePhotoBase64 { get; set; } = default!;

    /// <summary>   
    /// 其他附加信息
    /// </summary>
    public Dictionary<string, string>? AdditionalInfo { get; set; }
}