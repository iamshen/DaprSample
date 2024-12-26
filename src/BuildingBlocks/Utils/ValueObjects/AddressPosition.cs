#nullable disable
using DaprTool.BuildingBlocks.Utils.Enumerations;

namespace DaprTool.BuildingBlocks.Utils.ValueObjects;

/// <summary>
/// 地理位置
/// </summary>
/// <param name="Longitude">经度</param>
/// <param name="Latitude">纬度</param>
/// <param name="CoordinateSystem">坐标系</param>
public record AddressPosition(double Longitude, double Latitude, CoordinateSystem CoordinateSystem = CoordinateSystem.WGS84);