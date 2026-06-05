using System.Net.Http.Headers;
using SetExplorer.Client.Core.Scryfall.Symbology;

namespace SetExplorer.Client.Core.Scryfall;

public static class ScryfallExtensions
{
    public static IServiceCollection AddScryfallSearchClient(
        this IServiceCollection services)
    {
        services.AddMemoryCache();
        services
            .AddHttpClient<ScryfallSearchClient>((_, client) =>
            {
                client.BaseAddress = new Uri("https://api.scryfall.com");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Set-Explorer", "alpha"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        services
            .AddHttpClient<SymbologyClient>((_, client) =>
            {
                client.BaseAddress = new Uri("https://api.scryfall.com");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Set-Explorer", "alpha"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        return services;
    }
}