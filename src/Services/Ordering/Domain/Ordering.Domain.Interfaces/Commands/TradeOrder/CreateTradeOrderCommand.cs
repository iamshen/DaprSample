using FluentValidation;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Domain.Interfaces.Commands.TradeOrder;

/// <summary>
///     创建买卖料订单
/// </summary>
public class CreateTradeOrderCommand
{
    /// <summary>
    ///     购买人
    /// </summary>
    public Buyer Buyer { get; set; } = new();

    /// <summary>
    ///     销售员工
    /// </summary>
    public Seller Seller { get; set; } = new();

    /// <summary>
    ///     销售柜台
    /// </summary>
    public SaleStore SaleStore { get; set; } = new();

    /// <summary>
    ///     备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    ///     订单明细
    /// </summary>
    public IList<TradeItem> OrderItems { get; set; } = new List<TradeItem>();
}


/// <summary>
///     创建买卖料订单命令验证机
/// </summary>
public class CreateTradeOrderCommandValidator : AbstractValidator<CreateTradeOrderCommand>
{
    public CreateTradeOrderCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Buyer).NotNull().WithMessage("购买人不能为空");
        RuleFor(x => x.Buyer.BuyerId).NotNull().WithMessage("购买人不能为null").NotEmpty().WithMessage("购买人不能为空");
        RuleFor(x => x.Buyer.BuyerName).NotNull().WithMessage("购买人不能为null").NotEmpty().WithMessage("购买人不能为空");

        RuleFor(x => x.Seller).NotNull().WithMessage("销售人不能为空");
        RuleFor(x => x.Seller.SellerId).NotNull().WithMessage("销售人不能为null").NotEmpty().WithMessage("销售人不能为空");
        RuleFor(x => x.Seller.SellerName).NotNull().WithMessage("销售人不能为null").NotEmpty().WithMessage("销售人不能为空");

        RuleFor(x => x.SaleStore).NotNull().WithMessage("销售柜台不能为空");
        RuleFor(x => x.SaleStore.SellerId).NotNull().WithMessage("销售柜台不能为null").NotEmpty().WithMessage("销售柜台不能为空");
        RuleFor(x => x.SaleStore.SellerName).NotNull().WithMessage("销售柜台不能为null").NotEmpty().WithMessage("销售柜台不能为空");

        RuleFor(x => x.OrderItems)
            .NotEmpty().WithMessage("订单明细不能为空")
            .NotNull().WithMessage("订单明细不能为null")
            .Must(items => items.Any()).WithMessage("至少需要一条订单产品记录");
    }
}