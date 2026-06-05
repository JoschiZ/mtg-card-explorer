using Microsoft.Extensions.Caching.Memory;
using SetExplorer.Client.Core.Scryfall.Models;
using SetExplorer.Client.Core.Scryfall.Symbology.Models;

namespace SetExplorer.Client.Core.Scryfall.Symbology;

public class SymbologyClient : ScryfallBaseClient
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "scryfall-symbology";

    private static readonly MemoryCacheEntryOptions CacheEntryOptions = new()
    {
        SlidingExpiration = TimeSpan.FromMinutes(30),
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
    };
    public SymbologyClient(HttpClient httpClient, IMemoryCache cache) : base(httpClient)
    {
        _cache = cache;
    }
    
    public async Task<Dictionary<string, ScryfallMtgSymbol>> GetSymbolLookupAsync(CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(CacheKey, async _ =>
        {
            var response = await GetFromJsonAsync<ResultList<ScryfallMtgSymbol>>("/symbology", cancellationToken);
            return response.Data.ToDictionary(x => x.Symbol, x => x);
        }, CacheEntryOptions) ?? throw new NullReferenceException("Got null response from Scryfall");
    }
}