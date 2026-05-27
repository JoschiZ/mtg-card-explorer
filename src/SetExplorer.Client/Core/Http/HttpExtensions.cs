using SetExplorer.Client.Features.Collections;
using SetExplorer.Client.Features.Explorations;

namespace SetExplorer.Client.Core.Http;

internal static class HttpExtensions
{
    extension(IServiceCollection services)
    {
        public void AddApiClients(Uri baseUri)
        {
            services.AddTransient<AuthenticationDelegatingHandler>();
            services.RegisterApiClient<ICollectionsClient, CollectionsClient>(baseUri);
            services.RegisterApiClient<IExplorationsClient, ExplorationsClient>(baseUri);
        }

        private IHttpClientBuilder RegisterApiClient<TInterface, TClient>(Uri baseUri)
            where TInterface : class
            where TClient : class, TInterface
        {
            return services
                .AddHttpClient<TInterface, TClient>((provider, client) =>
                {
                    client.BaseAddress = new Uri(baseUri, "api/");;
                })
                .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
        }
    }
}