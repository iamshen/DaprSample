﻿using System.Reflection;
using System.Xml.XPath;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
///     Enums Name Schema Filter
/// </summary>
internal class XEnumNamesSchemaFilter : ISchemaFilter
{
    #region Constructors

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="options"><see cref="FixEnumsOptions" />.</param>
    /// <param name="configureOptions">An <see cref="Action{FixEnumsOptions}" /> to configure options for filter.</param>
    public XEnumNamesSchemaFilter(IOptions<FixEnumsOptions> options, Action<FixEnumsOptions> configureOptions = null)
    {
        if (options.Value != null)
        {
            configureOptions?.Invoke(options.Value);
            _includeXEnumDescriptions = options.Value.IncludeDescriptions;
            _includeXEnumRemarks = options.Value.IncludeXEnumRemarks;
            _descriptionSources = options.Value.DescriptionSource;
            _applyFiler = options.Value.ApplySchemaFilter;
            _xEnumNamesAlias = options.Value.XEnumNamesAlias;
            _xEnumDescriptionsAlias = options.Value.XEnumDescriptionsAlias;
            foreach (var filePath in options.Value.IncludedXmlCommentsPaths)
                if (File.Exists(filePath))
                    _xmlNavigators.Add(new XPathDocument(filePath).CreateNavigator());
        }
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Apply the filter.
    /// </summary>
    /// <param name="schema"><see cref="OpenApiSchema" />.</param>
    /// <param name="context"><see cref="SchemaFilterContext" />.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!_applyFiler)
            return;

        var typeInfo = context.Type.GetTypeInfo();
        var enumsArray = new OpenApiArray();
        var enumsDescriptionsArray = new OpenApiArray();
        if (typeInfo.IsEnum)
        {
            var names = Enum.GetNames(context.Type).Select(name => new OpenApiString(name)).ToList();
            enumsArray.AddRange(names);
            if (!schema.Extensions.ContainsKey(_xEnumNamesAlias) && enumsArray.Any())
                schema.Extensions.Add(_xEnumNamesAlias, enumsArray);

            if (_includeXEnumDescriptions)
            {
                enumsDescriptionsArray.AddRange(SwaggerEnumTypeExtensions.GetEnumValuesDescription(context.Type,
                    _descriptionSources, _xmlNavigators, _includeXEnumRemarks));
                if (!schema.Extensions.ContainsKey(_xEnumDescriptionsAlias) && enumsDescriptionsArray.Any())
                    schema.Extensions.Add(_xEnumDescriptionsAlias, enumsDescriptionsArray);
            }

            return;
        }

        // add "x-enumNames" or its alias for schema with generic types
        if (typeInfo.IsGenericType && !schema.Extensions.ContainsKey(_xEnumNamesAlias))
            foreach (var genericArgumentType in typeInfo.GetGenericArguments())
                if (genericArgumentType.IsEnum)
                    if (schema.Properties?.Count > 0)
                        foreach (var schemaProperty in schema.Properties)
                        {
                            var schemaPropertyValue = schemaProperty.Value;
                            var propertySchema = context.SchemaRepository.Schemas.FirstOrDefault(s =>
                                schemaPropertyValue.AllOf.FirstOrDefault(a => a.Reference.Id == s.Key) != null).Value;
                            if (propertySchema != null)
                            {
                                var names = Enum.GetNames(genericArgumentType).Select(name => new OpenApiString(name));
                                enumsArray.AddRange(names);
                                if (!schemaPropertyValue.Extensions.ContainsKey(_xEnumNamesAlias) && enumsArray.Any())
                                    schemaPropertyValue.Extensions.Add(_xEnumNamesAlias, enumsArray);

                                if (_includeXEnumDescriptions)
                                {
                                    enumsDescriptionsArray.AddRange(
                                        SwaggerEnumTypeExtensions.GetEnumValuesDescription(genericArgumentType,
                                            _descriptionSources, _xmlNavigators, _includeXEnumRemarks));
                                    if (!schemaPropertyValue.Extensions.ContainsKey(_xEnumDescriptionsAlias) &&
                                        enumsDescriptionsArray.Any())
                                        schemaPropertyValue.Extensions.Add(_xEnumDescriptionsAlias,
                                            enumsDescriptionsArray);
                                }

                                var description = propertySchema.AddEnumValuesDescription(_xEnumNamesAlias,
                                    _xEnumDescriptionsAlias, _includeXEnumDescriptions);
                                if (description != null)
                                {
                                    if (schemaPropertyValue.Description == null)
                                        schemaPropertyValue.Description = description;
                                    else if (!schemaPropertyValue.Description.Contains(description))
                                        schemaPropertyValue.Description += description;
                                }
                            }
                        }

        if (schema.Properties?.Count > 0)
            foreach (var schemaProperty in schema.Properties)
            {
                var schemaPropertyValue = schemaProperty.Value;
                var propertySchema = context.SchemaRepository.Schemas.FirstOrDefault(s =>
                    schemaPropertyValue.AllOf.FirstOrDefault(a => a.Reference.Id == s.Key) != null).Value;
                var description = propertySchema?.AddEnumValuesDescription(_xEnumNamesAlias, _xEnumDescriptionsAlias,
                    _includeXEnumDescriptions);
                if (description != null)
                {
                    if (schemaPropertyValue.Description == null)
                        schemaPropertyValue.Description = description;
                    else if (!schemaPropertyValue.Description.Contains(description))
                        schemaPropertyValue.Description += description;
                }
            }
    }

    #endregion

    #region Fields

    private readonly bool _includeXEnumDescriptions;
    private readonly bool _includeXEnumRemarks;
    private readonly string _xEnumNamesAlias;
    private readonly string _xEnumDescriptionsAlias;
    private readonly DescriptionSources _descriptionSources;
    private readonly bool _applyFiler;
    private readonly HashSet<XPathNavigator> _xmlNavigators = new();

    #endregion
}