using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace SetExplorer.Scryfall;

public abstract class ScryfallBaseClient
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        
    };

    protected ScryfallBaseClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected async Task<T> GetFromJsonAsync<T>([StringSyntax(StringSyntaxAttribute.Uri), UriString("GET")]string url, CancellationToken cancellationToken = default)
    {
        var httpResponse = await _httpClient.GetAsync(url, cancellationToken);
        return await httpResponse.Content.ReadFromJsonAsync<T>(Options, cancellationToken) 
               ?? throw new NullReferenceException("Retrieved null response for some rea");
    }
}