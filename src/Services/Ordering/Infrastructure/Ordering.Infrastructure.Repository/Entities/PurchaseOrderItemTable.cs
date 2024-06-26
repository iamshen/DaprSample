﻿using DaprTool.BuildingBlocks.Linq2DbAccessor.Converter;
using DaprTool.BuildingBlocks.Linq2DbAccessor.Data;
using LinqToDB;
using LinqToDB.Mapping;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Infrastructure.Repository.Entities;

/// <summary>
///     买料订单明细
/// </summary>
[Table(Schema = "public", Name = "t_purchase_order_item")]
public class PurchaseOrderItemTable : EntityBase
{
    /// <summary>
    ///     产品Id
    /// </summary>
    [Column("product_id", CanBeNull = true)]
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    ///     产品名称
    /// </summary>
    [Column("product_name", CanBeNull = false)]
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    ///     产品单价
    /// </summary>
    /// <remarks>产品单价：元/克 </remarks>
    [Column("unit_price", CanBeNull = false)]
    public decimal UnitPrice { get; set; }

    /// <summary>
    ///     工费
    /// </summary>
    /// <remarks>产品工费：元/克 </remarks>
    [Column("labor_cost", CanBeNull = false)]
    public double LaborCost { get; set; }

    /// <summary>
    ///     产品照片
    /// </summary>
    [Column("pictures", DataType = DataType.BinaryJson, CanBeNull = false)]
    [ValueConverter(ConverterType = typeof(JsonValueConverter<List<ReferResources>>))]
    public List<ReferResources> Pictures { get; set; } = [];
}