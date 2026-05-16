using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Scryfall.Models;

namespace SetExplorer.Client.Core.Scryfall;

public class ScryfallSearchClient : ScryfallBaseClient
{
    private readonly IMemoryCache _cache;
    public ScryfallSearchClient(HttpClient httpClient, IMemoryCache cache) : base(httpClient)
    {
        _cache = cache;
    }

    private static readonly MemoryCacheEntryOptions CacheEntryOptions = new()
    {
        SlidingExpiration = TimeSpan.FromMinutes(20),
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
    };

    private static string GetCacheKey(ScryfallCardId cardId) => $"card-{cardId}";
    
    public async Task<ScryfallCard?> GetCardAsync(ScryfallCardId cardId, CancellationToken cancellationToken = default)
    {
        const string path = "/cards/";
        return await _cache
            .GetOrCreateAsync(GetCacheKey(cardId), async _ =>
            {
                var response = await GetFromJsonAsync<ScryfallCard>(path + cardId, cancellationToken);
                return response;
            }, CacheEntryOptions);
    }


    public async IAsyncEnumerable<ScryfallCard> GetAllAsync(string query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await SearchAsync(query, null, cancellationToken);

        foreach (var card in response.Data)
        {
            _cache.Set(GetCacheKey(card.Id), card, CacheEntryOptions);
            yield return card;
        }

        while (response is { HasMore: true, NextPage: not null })
        {
            response =  await GetFromJsonAsync<ResultList<ScryfallCard>>(response.NextPage.AbsolutePath, cancellationToken);

            foreach (var card in response.Data)
            {
                _cache.Set(GetCacheKey(card.Id), card, CacheEntryOptions);
                yield return card;
            }
        }
    }

    public Task<ResultList<ScryfallCard>> SearchAsync(string query, int? page, CancellationToken cancellationToken = default)
    {
        const string path = "/cards/search";

        Dictionary<string, string?> queryParams = [];
        
        queryParams["q"] = query;
        if (page.HasValue)
        {
            queryParams["page"] = page.Value.ToString();
        }

        var uri = QueryHelpers.AddQueryString(path, queryParams);
        
        var result = GetFromJsonAsync<ResultList<ScryfallCard>>(uri, cancellationToken);
        return result;
    }
    
}