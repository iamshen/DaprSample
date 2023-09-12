using LinqToDB.Mapping;

namespace Common.Infrastructure.Linq2Db.Entities;

/// <summary>
///     省市区地址
/// </summary>
[Table(Schema = "gold_work", Name = "t_district")]
public class DistrictTable
{
    /// <summary>
    ///     省名称
    /// </summary>
    [Column("province_name", CanBeNull = false)]
    public string ProvinceName { get; set; } = string.Empty;

    /// <summary>
    ///     省编号
    /// </summary>
    [Column("province_id", CanBeNull = false)]

    public string ProvinceId { get; set; } = string.Empty;

    /// <summary>
    ///     市名称
    /// </summary>
    [Column("city_name", CanBeNull = false)]

    public string CityName { get; set; } = string.Empty;

    /// <summary>
    ///     市编号
    /// </summary>
    [Column("city_id", CanBeNull = false)]
    public string CityId { get; set; } = string.Empty;

    /// <summary>
    ///     区名称
    /// </summary>
    [Column("district_name", CanBeNull = false)]

    public string DistrictName { get; set; } = string.Empty;

    /// <summary>
    ///     区编号
    /// </summary>
    [Column("district_id", CanBeNull = false)]

    public string DistrictId { get; set; } = string.Empty;

    /// <summary>
    ///     乡镇名称
    /// </summary>
    [Column("town_name", CanBeNull = false)]

    public string TownName { get; set; } = string.Empty;

    /// <summary>
    ///     乡镇编号
    /// </summary>
    [Column("town_id", CanBeNull = false)]
    public string TownId { get; set; } = string.Empty;

    /// <summary>
    ///     村名称
    /// </summary>
    [Column("village_name", CanBeNull = false)]

    public string VillageName { get; set; } = string.Empty;

    /// <summary>
    ///     村编号
    /// </summary>
    [Column("village_id", CanBeNull = false)]

    public string VillageId { get; set; } = string.Empty;

    /// <summary>
    ///     类型名称
    /// </summary>
    /// <remarks>（村庄、镇乡结合区、镇中心区、城乡结合区、乡中心区、特殊区域、主城区）</remarks>
    [Column("type_name", CanBeNull = false)]

    public string TypeName { get; set; } = string.Empty;

    /// <summary>
    ///     类型编号
    /// </summary>
    [Column("type_id", CanBeNull = false)]
    public string TypeId { get; set; } = string.Empty;

    /// <summary>
    ///     纬度
    /// </summary>
    [Column("lng", CanBeNull = false)]
    public decimal Lng { get; set; }

    /// <summary>
    ///     经度
    /// </summary>
    [Column("lat", CanBeNull = false)]
    public decimal Lat { get; set; }
}