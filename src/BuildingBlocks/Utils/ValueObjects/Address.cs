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
    public string ProvinceCode { get; set; }

    /// <summary>
    ///     省
    /// </summary>
    [Display(Name = "省")]
    public string Province { get; set; }

    /// <summary>
    ///     市 地址编码
    /// </summary>
    [Display(Name = "市 地址编码")]
    public string CityCode { get; set; }

    /// <summary>
    ///     市
    /// </summary>
    [Display(Name = "市")]
    public string City { get; set; }

    /// <summary>
    ///     区 编码
    /// </summary>
    [Display(Name = "区 编码")]
    public string AreaCode { get; set; }

    /// <summary>
    ///     区
    /// </summary>
    [Display(Name = "区")]
    public string Area { get; set; }

    /// <summary>
    ///     街道/镇 地址编码
    /// </summary>
    [Display(Name = "街道/镇 地址编码")]
    public string StreetCode { get; set; }

    /// <summary>
    ///     街道/镇
    /// </summary>
    [Display(Name = "街道/乡镇")]
    public string Street { get; set; }

    /// <summary>
    ///     居委会/村庄 地址编码
    /// </summary>
    [Display(Name = "居委会/村庄 地址编码")]
    public string VillageCode { get; set; }

    /// <summary>
    ///     居委会/村庄
    /// </summary>
    [Display(Name = "居委会/村庄")]
    public string Village { get; set; }

    /// <summary>
    ///     详细地址
    /// </summary>
    [Display(Name = "详细地址")]
    public string Detailed { get; set; }

    /// <summary>
    ///     转字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Province}{City}{Area}{Street}{Detailed}";
    }
}

/// <summary>
/// 地理位置
/// </summary>
/// <param name="Longitude">经度</param>
/// <param name="Latitude">纬度</param>
public record AddressPosition(double Longitude, double Latitude);