using DaprTool.BuildingBlocks.Utils;
using DaprTool.BuildingBlocks.Utils.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace DaprTool.BuildingBlocks.Domain.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{

    public override Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default)
    {
        return base.ValidateAsync(context, cancellation);
    }

    public override ValidationResult Validate(ValidationContext<T> context)
    {
        var validateResult = base.Validate(context);
        if (!validateResult.IsValid)
        {
            var message = string.Join(";", validateResult.Errors.Select(x => x.ErrorMessage));
            throw new GlobalException(ErrorCode.ArgumentException, message);
        }

        return validateResult;
    }
}
