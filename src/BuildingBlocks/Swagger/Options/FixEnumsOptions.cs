using System.ComponentModel;

namespace Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
///     Various configuration properties for fixing enums.
/// </summary>
public class FixEnumsOptions
{
    #region Properties

    /// <summary>
    ///     Included file paths with XML comments.
    /// </summary>
    public HashSet<string> IncludedXmlCommentsPaths { get; } = new();

    /// <summary>
    ///     Include descriptions from <see cref="DescriptionAttribute" /> or xml comments. Default value is false.
    /// </summary>
    public bool IncludeDescriptions { get; set; } = false;

    /// <summary>
    ///     Include remarks for descriptions from xml comments. Default value is false.
    /// </summary>
    public bool IncludeXEnumRemarks { get; set; } = false;

    /// <summary>
    ///     Source to get descriptions. Default value is <see cref="DescriptionSources.DescriptionAttributes" />.
    /// </summary>
    public DescriptionSources DescriptionSource { get; set; } = DescriptionSources.DescriptionAttributes;

    /// <summary>
    ///     Apply fix enum filter to OpenApi schema. Default value is true.
    ///     <para>
    ///         Equivalent to "options.SchemaFilter&lt;XEnumNamesSchemaFilter&gt;();"
    ///     </para>
    /// </summary>
    public bool ApplySchemaFilter { get; set; } = true;

    /// <summary>
    ///     Apply fix enum filter to OpenApi parameters. Default value is true.
    ///     <para>
    ///         Equivalent to "options.ParameterFilter&lt;XEnumNamesParameterFilter&gt;();"
    ///     </para>
    /// </summary>
    public bool ApplyParameterFilter { get; set; } = true;

    /// <summary>
    ///     Apply fix enum filter to OpenApi document. Default value is true.
    ///     <para>
    ///         Equivalent to "options.DocumentFilter&lt;DisplayEnumsWithValuesDocumentFilter&gt;();"
    ///     </para>
    /// </summary>
    public bool ApplyDocumentFilter { get; set; } = true;

    /// <summary>
    ///     Alias for replacing "x-enumNames" in swagger documentation.
    /// </summary>
    public string XEnumNamesAlias { get; set; } = "x-enumNames";

    /// <summary>
    ///     Alias for replacing "x-enumDescriptions" in swagger documentation.
    /// </summary>
    public string XEnumDescriptionsAlias { get; set; } = "x-enumDescriptions";

    #endregion
}

/// <summary>
/// </summary>
public static class FixEnumsOptionsExtensions
{
    #region Extension methods

    /// <summary>
    ///     Include XML comments from <paramref name="fullPath" />.
    /// </summary>
    /// <param name="options"><see cref="FixEnumsOptions" />.</param>
    /// <param name="fullPath">Full file path with XML comments.</param>
    /// <returns>
    ///     Returns <see cref="FixEnumsOptions" />.
    /// </returns>
    public static FixEnumsOptions IncludeXmlCommentsFrom(this FixEnumsOptions options, string fullPath)
    {
        if (!options.IncludedXmlCommentsPaths.Contains(fullPath))
            options.IncludedXmlCommentsPaths.Add(fullPath);

        return options;
    }

    #endregion
}