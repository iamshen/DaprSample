using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class ResultExtensions
{
    public static IActionResult ToOkResult<TResult, TOutput>(this Result<TResult> result, Func<TResult, TOutput> mapper)
    {
        return result.Match<IActionResult>(res =>
        {
            var response = mapper(res);
            return new OkObjectResult(response);
        }, ex => throw ex);
    }


    /// <summary>
    ///     ToCreatedResult
    /// </summary>
    /// <param name="result"></param>
    /// <param name="mapper"></param>
    /// <param name="uri"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    /// <returns></returns>
    public static IActionResult ToCreatedResult<TResult, TOutput>(this Result<TResult> result,
        Func<TResult, TOutput> mapper,
        string uri)
    {

        return result.Match<IActionResult>(res =>
        {
            var response = mapper(res);
            return new CreatedResult(uri, response);
        }, ex =>
        {
            throw ex;
            // if (ex is ValidationException validationException)
            // {
            //     return new BadRequestObjectResult(validationException.ToProblemDetails());
            // }

            // // TODO: Others Exceptions
            // return new StatusCodeResult(500);
        });
    }
}
