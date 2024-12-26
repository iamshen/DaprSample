namespace DaprTool.BuildingBlocks.Utils.Enumerations;

#region 坐标系

/// <summary>
/// 坐标系
/// 地理空间参考系统（Spatial Reference System, SRS），
/// 它定义了如何将地球上的位置映射到二维或三维坐标。
/// 不同的应用和国家可能会使用不同的坐标系，
/// 下面是一些常见的坐标系
/// </summary>
public enum CoordinateSystem
{
    /// <summary>WGS 84坐标系</summary>
    WGS84 = 1 ,
    /// <summary>GCJ-02坐标系</summary>
    GCJ02,
    /// <summary>BD-09坐标系</summary>
    BD09,
    /// <summary>EPSG:3857坐标系</summary>
    EPSG3857,
    /// <summary>CGCS2000坐标系</summary>
    CGCS2000,
}

#endregion