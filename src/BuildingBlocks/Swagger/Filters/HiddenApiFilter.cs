#nullable disable


using Microsoft.OpenApi.Models;

namespace Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
///     标记此过滤器，API将在Swagger界面中隐藏
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class HiddenApiAttribute : Attribute
{
}

/// <summary>
///     Swagger隐藏过滤器
/// </summary>
public class HiddenApiFilter : IDocumentFilter
{
    /// <summary>
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
            if (apiDescription.TryGetMethodInfo(out var method))
                if (method.ReflectedType.IsDefined(typeof(HiddenApiAttribute), false) ||
                    method.IsDefined(typeof(HiddenApiAttribute), false))
                {
                    var key = $"/{apiDescription.RelativePath}";
                    if (key.Contains("?"))
                    {
                        var index = key.IndexOf("?", StringComparison.Ordinal);
                        key = key.Substring(0, index);
                    }

                    swaggerDoc.Paths.Remove(key);
                }
    }
}