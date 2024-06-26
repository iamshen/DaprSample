﻿using DaprTool.BuildingBlocks.Linq2DbAccessor.Converter;
using DaprTool.BuildingBlocks.Linq2DbAccessor.Data;
using LinqToDB;
using LinqToDB.Mapping;
using Ordering.Infrastructure.Shared.Enumerations.PurchaseOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Infrastructure.Repository.Entities;

/// <summary>
///     买料订单表
/// </summary>
[Table(Schema = "public", Name = "t_purchase_order")]
public class PurchaseOrderTable : EntityBase
{
    /// <summary>
    ///     订单号
    /// </summary>
    [Column("order_no", CanBeNull = false)]
    public string OrderNo { get; set; } = string.Empty;

    /// <summary>
    ///     订单状态
    /// </summary>
    [Column("order_status", DataType = DataType.Int16, CanBeNull = false)]
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;

    /// <summary>
    ///     支付状态
    /// </summary>
    [Column("pay_status", DataType = DataType.Int16, CanBeNull = false)]
    public PayStatus PayStatus { get; set; } = PayStatus.Created;

    /// <summary>
    ///     交易状态
    /// </summary>
    [Column("trade_status", DataType = DataType.Int16, CanBeNull = false)]
    public TradeStatus TradeStatus { get; set; } = TradeStatus.Created;

    /// <summary>
    ///     购买人
    /// </summary>
    [Column("buyer", DataType = DataType.BinaryJson, CanBeNull = false)]
    [ValueConverter(ConverterType = typeof(JsonValueConverter<Buyer>))]
    public Buyer Buyer { get; set; } = new();

    /// <summary>
    ///     销售员工
    /// </summary>
    [Column("seller", DataType = DataType.BinaryJson, CanBeNull = false)]
    [ValueConverter(ConverterType = typeof(JsonValueConverter<Seller>))]
    public Seller Seller { get; set; } = new();

    /// <summary>
    ///     销售门店
    /// </summary>
    [Column("sale_store", DataType = DataType.BinaryJson, CanBeNull = false)]
    [ValueConverter(ConverterType = typeof(JsonValueConverter<SaleStore>))]
    public SaleStore SaleStore { get; set; } = new();

    /// <summary>
    ///     备注
    /// </summary>
    [Column("remark", CanBeNull = true)]
    public string? Remark { get; set; }
}