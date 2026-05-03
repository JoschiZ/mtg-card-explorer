using SetExplorer.Client.Core.Scryfall;

namespace SetExplorer.Client.Core;

public static class SharedConfigurationExtensions
{
    public static IServiceCollection AddSharedConfiguration(this IServiceCollection services)
    {
        services.AddScryfallSearchClient();
#if DEBUG
        
        services.AddExtendedHttpClientLogging();
#endif
        return services;
    }
}