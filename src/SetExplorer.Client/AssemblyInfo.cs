using Vogen;

[assembly: VogenDefaults(
    customizations: Customizations.AddFactoryMethodForGuids,
    openApiSchemaCustomizations: OpenApiSchemaCustomizations.GenerateOpenApiMappingExtensionMethod,
    toPrimitiveCasting: CastOperator.Explicit)]