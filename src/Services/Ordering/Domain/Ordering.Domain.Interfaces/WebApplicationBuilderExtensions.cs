using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Interfaces.Validators.PurchaseOrder;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     添加 命令验证器
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAppValidators(this WebApplicationBuilder builder)
    {
        // Add  Validators 
        builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>(ServiceLifetime.Transient);
    }
}