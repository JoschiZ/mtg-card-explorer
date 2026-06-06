using CardExplorer.Client.Core.Scryfall;

namespace CardExplorer.Client.Core;

public static class SharedConfigurationExtensions
{
    public static IServiceCollection AddSharedConfiguration(this IServiceCollection services)
    {
        services.AddScryfallSearchClient();
        services.AddRedaction();
#if DEBUG
        
        services.AddExtendedHttpClientLogging();
#endif
        return services;
    }
}