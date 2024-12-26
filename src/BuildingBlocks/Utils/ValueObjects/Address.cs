#nullable disable

using System.ComponentModel.DataAnnotations;

namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

/// <summary>
///     地址对象
/// </summary>
public record Address
{
    /// <summary>
    ///     省 地址编码
    /// </summary>
    [Display(Name = "省 地址编码")]
    public string ProvinceCode { get; init; }

    /// <summary>
    ///     省
    /// </summary>
    [Display(Name = "省")]
    public string Province { get; init; }

    /// <summary>
    ///     市 地址编码
    /// </summary>
    [Display(Name = "市 地址编码")]
    public string CityCode { get; init; }

    /// <summary>
    ///     市
    /// </summary>
    [Display(Name = "市")]
    public string City { get; init; }

    /// <summary>
    ///     区 编码
    /// </summary>
    [Display(Name = "区 编码")]
    public string AreaCode { get; set; }

    /// <summary>
    ///     区
    /// </summary>
    [Display(Name = "区")]
    public string Area { get; init; }

    /// <summary>
    ///     街道/镇 地址编码
    /// </summary>
    [Display(Name = "街道/镇 地址编码")]
    public string StreetCode { get; init; }

    /// <summary>
    ///     街道/镇
    /// </summary>
    [Display(Name = "街道/乡镇")]
    public string Street { get; init; }

    /// <summary>
    ///     居委会/村庄 地址编码
    /// </summary>
    [Display(Name = "居委会/村庄 地址编码")]
    public string VillageCode { get; init; }

    /// <summary>
    ///     居委会/村庄
    /// </summary>
    [Display(Name = "居委会/村庄")]
    public string Village { get; init; }

    /// <summary>
    ///     详细地址
    /// </summary>
    [Display(Name = "详细地址")]
    public string Detailed { get; init; }

    /// <summary>
    ///     地理坐标
    /// </summary>
    public AddressPosition Position { get; init; }

    /// <summary>
    ///     转字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Province}{City}{Area}{Street}{Detailed}";
    }
}