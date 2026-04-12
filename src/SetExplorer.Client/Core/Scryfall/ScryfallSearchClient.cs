using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.WebUtilities;
using SetExplorer.Scryfall.Models;

namespace SetExplorer.Scryfall;

public class ScryfallSearchClient : ScryfallBaseClient
{
    public ScryfallSearchClient(HttpClient httpClient) : base(httpClient)
    {
    }


    public async IAsyncEnumerable<Card> GetAllAsync(string query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await SearchAsync(query, null, cancellationToken);

        foreach (var card in response.Data)
        {
            yield return card;
        }

        while (response is { HasMore: true, NextPage: not null })
        {
            response =  await GetFromJsonAsync<ResultList<Card>>(response.NextPage.AbsolutePath, cancellationToken);

            foreach (var card in response.Data)
            {
                yield return card;
            }
        }
    }

    public Task<ResultList<Card>> SearchAsync(string query, int? page, CancellationToken cancellationToken = default)
    {
        const string path = "/cards/search";

        Dictionary<string, string?> queryParams = [];
        
        queryParams["q"] = query;
        if (page.HasValue)
        {
            queryParams["page"] = page.Value.ToString();
        }

        var uri = QueryHelpers.AddQueryString(path, queryParams);
        
        var result = GetFromJsonAsync<ResultList<Card>>(uri, cancellationToken);
        return result;
    }
    
}