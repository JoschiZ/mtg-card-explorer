using System.Net.Http.Headers;

namespace SetExplorer.Scryfall;

public static class ScryfallExtensions
{
    public static IServiceCollection AddScryfallSearchClient(
        this IServiceCollection services)
    {
        services
            .AddHttpClient<ScryfallSearchClient>((provider, client) =>
            {
                client.BaseAddress = new Uri("https://api.scryfall.com");
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CardExplorer", "alpha"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        return services;
    }
}