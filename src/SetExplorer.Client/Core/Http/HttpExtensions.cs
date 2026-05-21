namespace SetExplorer.Client.Core.Http;

internal static class HttpExtensions
{
    public static void AddHttpClients(this IServiceCollection services)
    {
        services.RegisterApiClient<ICollectionsClient, CollectionsClient>();
        services.RegisterApiClient<IExplorationsClient, ExplorationsClient>();
    }

    private static IHttpClientBuilder RegisterApiClient<TInterface, TClient>(this IServiceCollection services)
        where TInterface : class
        where TClient : class, TInterface
    {
        return services.AddHttpClient<TInterface, TClient>()
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
    }
}