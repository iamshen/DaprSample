using DaprTool.BuildingBlocks.Utils;
using DaprTool.BuildingBlocks.Utils.Exceptions;

namespace FluentValidation;

public static class ValidatorExtensions
{
    public static void ValidateAndThrowGlobal<T>(this IValidator<T> validator, T instance)
    {
        validator.Validate(instance);

        var validateResult = validator.Validate(instance);
        if (!validateResult.IsValid)
        {
            var message = string.Join(";", validateResult.Errors.Select(x => x.ErrorMessage));
            throw new GlobalException(ErrorCode.ArgumentException, message);
        }
    }

    public static async Task ValidateAndThrowGlobalAsync<T>(this IValidator<T> validator, T instance, CancellationToken cancellationToken = default(CancellationToken))
    {
        var validateResult = await validator.ValidateAsync(instance);
        if (!validateResult.IsValid)
        {
            var message = string.Join(";", validateResult.Errors.Select(x => x.ErrorMessage));
            throw new GlobalException(ErrorCode.ArgumentException, message);
        }
    }
}