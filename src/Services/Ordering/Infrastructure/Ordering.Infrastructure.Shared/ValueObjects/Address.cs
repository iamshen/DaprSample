using System.ComponentModel.DataAnnotations;

namespace Ordering.Infrastructure.Shared.ValueObjects;

/// <summary>
///     地址对象
/// </summary>
public class Address
{
    /// <summary>
    ///     国 编码
    /// </summary>
    [Display(Name = "国编码")]
    public string CountryCode { get; set; } = "zh-cn";

    /// <summary>
    ///     国
    /// </summary>
    [Display(Name = "国")]
    public string Country { get; set; } = "中国";

    /// <summary>
    ///     省 地址编码
    /// </summary>
    [Display(Name = "省 地址编码")]
    public string ProvinceCode { get; set; } = string.Empty;

    /// <summary>
    ///     省
    /// </summary>
    [Display(Name = "省")]
    public string Province { get; set; } = string.Empty;

    /// <summary>
    ///     市 地址编码
    /// </summary>
    [Display(Name = "市 地址编码")]
    public string CityCode { get; set; } = string.Empty;

    /// <summary>
    ///     市
    /// </summary>
    [Display(Name = "市")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    ///     区 编码
    /// </summary>
    [Display(Name = "区 编码")]
    public string AreaCode { get; set; } = string.Empty;

    /// <summary>
    ///     区
    /// </summary>
    [Display(Name = "区")]
    public string Area { get; set; } = string.Empty;

    /// <summary>
    ///     街道/镇 地址编码
    /// </summary>
    [Display(Name = "街道/镇 地址编码")]
    public string StreetCode { get; set; } = string.Empty;

    /// <summary>
    ///     街道/镇
    /// </summary>
    [Display(Name = "街道/乡镇")]
    public string Street { get; set; } = string.Empty;

    /// <summary>
    ///     详细地址
    /// </summary>
    [Display(Name = "详细地址")]
    public string Detail { get; set; } = string.Empty;

    /// <summary>
    ///     经度
    /// </summary>
    [Display(Name = "经度")]
    public double Longitudes { get; set; }

    /// <summary>
    ///     纬度
    /// </summary>
    [Display(Name = "纬度")]
    public double Latitude { get; set; }

    /// <summary>
    ///     转字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Country}{Province}{City}{Area}{Street}{Detail}";
    }
}