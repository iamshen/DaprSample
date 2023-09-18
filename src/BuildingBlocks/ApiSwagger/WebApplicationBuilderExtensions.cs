using DaprTool.BuildingBlocks.ApiSwagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

#region Swagger相关的扩展方法

/// <summary>
///     Swagger ServiceCollection Extensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     Add GoldCloud Swagger
    /// </summary>
    /// <param name="builder">      </param>
    /// <returns> </returns>
    public static void AddAppSwagger(this WebApplicationBuilder builder)
    {
        var swaggerOptions = builder.Configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>();
        if (swaggerOptions is null) return;
        builder.Services.AddSingleton(swaggerOptions);
        var fixEnumOptions = ConfigureFixEnumsOptions(builder.Services);
        builder.Services.AddAppSwagger(swaggerOptions, fixEnumOptions);
    }

    /// <summary>
    ///     Add GoldCloud Swagger
    /// </summary>
    /// <param name="services">       </param>
    /// <param name="swaggerOptions"> </param>
    /// <param name="configureOptions"> </param>
    /// <returns> </returns>
    private static void AddAppSwagger(this IServiceCollection services, SwaggerOptions swaggerOptions,
        Action<FixEnumsOptions>? configureOptions = null)
    {
        if (swaggerOptions?.Enabled != true) return;

        services.AddSwaggerGen(options =>
        {
            if (swaggerOptions.Endpoints?.Count > 0)
            {
                foreach (var endpoint in swaggerOptions.Endpoints)
                    options.SwaggerDoc($"{endpoint.Name}",
                        new OpenApiInfo { Title = endpoint.Title, Version = endpoint.Name });

                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var method))
                        return false;

                    var versions = method.DeclaringType.GetCustomAttributes(typeof(ApiExplorerSettingsAttribute), false)
                        .Cast<ApiExplorerSettingsAttribute>().Select(m => m.GroupName).ToArray();
                    if (version.ToLower() == "default" && versions.Length == 0)
                        return true;

                    return versions.Any(m => m.ToString() == version);
                });
            }

            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList()
                .ForEach(file => options.IncludeXmlComments(file));
            // swagger 权限Token
            options.AddSecurityDefinition("IdentityServer4", new OpenApiSecurityScheme
            {
                Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "IdentityServer4" }
                    },
                    new[] { "readAccess", "writeAccess" }
                }
            });

            options.CustomSchemaIds(type => type.FullName);

            // 枚举类
            options.AddEnumsWithValuesFixFilters(configureOptions);

            options.DocumentFilter<HiddenApiFilter>();
        });
    }

    /// <summary>
    ///     Configure Swagger FixEnums Options
    /// </summary>
    /// <param name="services"></param>
    private static Action<FixEnumsOptions>? ConfigureFixEnumsOptions(IServiceCollection services)
    {
        void ConfigureOptions(FixEnumsOptions o)
        {
            // add schema filter to fix enums (add 'x-enumNames' for NSwag or its alias from
            // XEnumNamesAlias) in schema
            o.ApplySchemaFilter = true;

            // alias for replacing 'x-enumNames' in swagger document
            o.XEnumNamesAlias = "x-enum-varnames";

            // alias for replacing 'x-enumDescriptions' in swagger document
            o.XEnumDescriptionsAlias = "x-enum-descriptions";

            // add parameter filter to fix enums (add 'x-enumNames' for NSwag or its alias
            // from XEnumNamesAlias) in schema parameters
            o.ApplyParameterFilter = true;

            // add document filter to fix enums displaying in swagger document
            o.ApplyDocumentFilter = true;

            // add descriptions from DescriptionAttribute or xml-comments to fix enums (add
            // 'x-enumDescriptions' or its alias from XEnumDescriptionsAlias for schema
            // extensions) for applied filters
            o.IncludeDescriptions = true;

            // add remarks for descriptions from xml-comments
            o.IncludeXEnumRemarks = true;

            // get descriptions from DescriptionAttribute then from xml-comments
            o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

            // get descriptions from xml-file comments on the specified path should use
            // "options.IncludeXmlComments(xmlFilePath);" before o.IncludeXmlCommentsFrom(xmlFilePath);

            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml")
                .ToList()
                .ForEach(xmlFilePath => o.IncludeXmlCommentsFrom(xmlFilePath));
            // the same for another xml-files...
        }

        services.Configure<FixEnumsOptions>(ConfigureOptions);

        return ConfigureOptions;
    }

    /// <summary>
    ///     Add Swagger Enums Values Fix Filters
    /// </summary>
    /// <param name="swaggerGenOptions"> </param>
    /// <param name="configureOptions">  </param>
    /// <returns> </returns>
    private static void AddEnumsWithValuesFixFilters(this SwaggerGenOptions swaggerGenOptions,
        Action<FixEnumsOptions>? configureOptions = null)
    {
        // local function
        void EmptyAction(FixEnumsOptions x)
        {
        }

        swaggerGenOptions.SchemaFilter<XEnumNamesSchemaFilter>(configureOptions ?? EmptyAction);
        swaggerGenOptions.ParameterFilter<XEnumNamesParameterFilter>(configureOptions ?? EmptyAction);
        swaggerGenOptions.DocumentFilter<DisplayEnumsWithValuesDocumentFilter>();
    }
}

#endregion