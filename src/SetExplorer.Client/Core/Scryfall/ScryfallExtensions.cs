using System.Net.Http.Headers;

namespace SetExplorer.Scryfall;

public static class ScryfallExtensions
{
    public static IServiceCollection AddScryfallSearchClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddHttpClient<ScryfallSearchClient>((provider, client) =>
            {
                client.BaseAddress = new Uri("api.scryfall.com");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CardExplorer", "alpha"));
            });
        
        return services;
    }
}