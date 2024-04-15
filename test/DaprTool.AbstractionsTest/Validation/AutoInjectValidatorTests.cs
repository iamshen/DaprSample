using DaprTool.AbstractionsTest.Base;
using DaprTool.BuildingBlocks.Utils.Exceptions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Interfaces.Commands.PurchaseOrder;

namespace DaprTool.AbstractionsTest.Validation;

/// <summary>
///     自动注入验证器
/// </summary>
/// <param name="fixture"></param>
public class AutoInjectValidatorTests(ValidationDependencySetupFixture fixture)
    : IClassFixture<ValidationDependencySetupFixture>
{
    [Fact(DisplayName = "01. 自动注入验证器测试")]
    public async Task Test1()
    {
        // arrange
        var command = new CreateOrderCommand();

        using var scope = fixture.ServiceProvider.CreateScope();
        var validator = scope.ServiceProvider.GetRequiredService<IValidator<CreateOrderCommand>>();

        Assert.NotNull(validator);

        _ = await Assert.ThrowsAsync<GlobalException>(() => validator.ValidateAndThrowGlobalAsync(command));
    }

    [Fact(DisplayName = "02. 自动注入验证器测试")]
    public async Task Test2()
    {
        // arrange
        var input = new TestRecord
        {
            Name = null,
            Age = 0
        };

        using var scope = fixture.ServiceProvider.CreateScope();
        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TestRecord>>();

        Assert.NotNull(validator);

        _ = await Assert.ThrowsAsync<GlobalException>(() => validator.ValidateAndThrowGlobalAsync(input));
    }
}

public class TestRecord
{
    public string? Name { get; set; } = string.Empty;

    public int Age { get; set; }
}

public class CreateTestRecordValidator : AbstractValidator<TestRecord>
{
    public CreateTestRecordValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");
        RuleFor(x => x.Age).NotNull().LessThanOrEqualTo(0).WithMessage("无效的年龄");
    }
}