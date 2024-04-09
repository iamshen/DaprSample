using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Microsoft.AspNetCore.Mvc;

#region ApplicationBuilder扩展方法

/// <summary>
/// ApplicationBuilder扩展方法
/// </summary>
public static class IApplicationBuilderExtensions
{
    #region 配置多语言

    /// <summary>
    /// 配置多语言
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseLanguage(this IApplicationBuilder builder)
    {
        var cultureList = new List<CultureInfo>();
        var options = new RequestLocalizationOptions();

        cultureList.Add(new CultureInfo("zh-hans"));
        //cultureList.Add(new CultureInfo("zh-cn"));
        //cultureList.Add(new CultureInfo("zh"));

        cultureList.Add(new CultureInfo("zh-hant"));

        cultureList.Add(new CultureInfo("en"));
        cultureList.Add(new CultureInfo("en-us"));

        options.DefaultRequestCulture = new RequestCulture("zh-hans", "zh-hans");
        options.SupportedCultures = cultureList;
        options.SupportedUICultures = cultureList;

        //新建基于Query String的多语言提供程序
        var queryStringProvider = new QueryStringRequestCultureProvider
        {
            QueryStringKey = "lang",
            UIQueryStringKey = "lang",
        };
        //将提供程序插入到集合的第一个位置，这样优先使用
        options.RequestCultureProviders.Insert(0, queryStringProvider);

        return builder.UseRequestLocalization(options);
    }

    #endregion


    #region 设置追踪信息

    /// <summary>
    /// 设置追踪信息
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseTraceInfo(this IApplicationBuilder builder)
    {
        //通过中间件设置追中追踪ID,如果请求头中设置了"Request-Trace-Id"则使用设置的RequestTraceIdKey，否则自己生成一个
        builder.Use(async (context, next) =>
        {
            if (context.Request.Headers.TryGetValue(AppConstants.RequestTraceIdKey, out var value) && !string.IsNullOrEmpty(value))
                context.TraceIdentifier = value!;
            else
                context.TraceIdentifier = Guid.NewGuid().ToString();

            await next.Invoke();
        });
        return builder;
    }

    #endregion
}

#endregion
