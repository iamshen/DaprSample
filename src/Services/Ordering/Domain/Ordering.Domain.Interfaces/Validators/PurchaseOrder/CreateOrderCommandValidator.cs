using FluentValidation;
using Ordering.Domain.Interfaces.Commands.PurchaseOrder;

namespace Ordering.Domain.Interfaces.Validators.PurchaseOrder;

/// <summary>
///     创建买料订单命令验证机
/// </summary>
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Buyer).NotNull().WithMessage("购买人不能为空");
        RuleFor(x => x.Buyer.BuyerId).NotNull().WithMessage("购买人不能为null").NotEmpty().WithMessage("购买人不能为空")
            .When(x => x.Buyer != null);
        RuleFor(x => x.Buyer.BuyerName).NotNull().WithMessage("购买人不能为null").NotEmpty().WithMessage("购买人不能为空")
            .When(x => x.Buyer != null);

        RuleFor(x => x.Seller).NotNull().WithMessage("销售不能为空");
        RuleFor(x => x.Seller.SellerId).NotNull().WithMessage("销售Id不能为null").NotEmpty().WithMessage("销售Id不能为空")
            .When(x => x.Seller != null);
        RuleFor(x => x.Seller.SellerName).NotNull().WithMessage("销售姓名不能为null").NotEmpty().WithMessage("销售姓名不能为空")
            .When(x => x.Seller != null);

        RuleFor(x => x.SaleStore).NotNull().WithMessage("销售门店不能为空");
        RuleFor(x => x.SaleStore.StoreId).NotNull().WithMessage("销售门店不能为null").NotEmpty().WithMessage("销售门店不能为空")
            .When(x => x.SaleStore != null);
        RuleFor(x => x.SaleStore.StoreName).NotNull().WithMessage("销售门店不能为null").NotEmpty().WithMessage("销售门店不能为空")
            .When(x => x.SaleStore != null);

        RuleFor(x => x.OrderItems)
            .NotEmpty().WithMessage("订单明细不能为空")
            .NotNull().WithMessage("订单明细不能为null")
            .Must(items => items.Any()).WithMessage("至少需要一条订单产品记录");
    }
}