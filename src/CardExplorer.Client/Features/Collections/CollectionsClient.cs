using System.Net.Http.Json;

namespace CardExplorer.Client.Features.Collections;

internal class CollectionsClient(HttpClient httpClient) : ICollectionsClient
{
    public async Task<CardCollectionDto[]> GetCollectionsAsync(GetCollectionsRequest request, CancellationToken ct = default)
    {
        var url = $"collections?Name={Uri.EscapeDataString(request.Name ?? string.Empty)}";
        return await httpClient.GetFromJsonAsync<CardCollectionDto[]>(url, ct) ?? [];
    }

    public async Task<CardCollectionDto?> CreateCollectionAsync(CreateCollectionRequest request, CancellationToken ct = default)
    {
        var response = await httpClient.PostAsJsonAsync("collections", request, ct);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CardCollectionDto>(ct);
        }
        return null;
    }

    public async Task AddCardToCollectionAsync(AddCardToCollectionRequest request, CancellationToken ct = default)
    {
        var url = $"collections/{request.CollectionId}/cards/{request.CardId}";
        await httpClient.PostAsync(url, null, ct);
    }

    public async Task RemoveCardFromCollectionAsync(RemoveCardFromCollectionRequest request, CancellationToken ct = default)
    {
        var url = $"collections/{request.CollectionId}/cards/{request.CardId}";
        await httpClient.DeleteAsync(url, ct);
    }
}
