using DaprTool.BuildingBlocks.Linq2DbAccessor.Converter;
using DaprTool.BuildingBlocks.Linq2DbAccessor.Data;
using LinqToDB;
using LinqToDB.Mapping;
using Ordering.Infrastructure.Shared.Enumerations.TradeOrder;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Infrastructure.Linq2Db.Entities;

/// <summary>
///     买卖料订单表
/// </summary>
[Table(Schema = "gold_work", Name = "t_trade_order")]
public class TradeOrderTable : EntityBase
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
    ///     销售柜台
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