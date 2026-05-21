using System.Net.Http.Json;
using SetExplorer.Client.Features.Collections;

namespace SetExplorer.Client.Core.Http;

public interface ICollectionsClient
{
    Task<List<CardCollectionDto>> GetCollectionsAsync(GetCollectionsRequest request, CancellationToken ct = default);
    Task<CardCollectionDto?> CreateCollectionAsync(CreateCollectionRequest request, CancellationToken ct = default);
    Task AddCardToCollectionAsync(AddCardToCollectionRequest request, CancellationToken ct = default);
    Task RemoveCardFromCollectionAsync(RemoveCardFromCollectionRequest request, CancellationToken ct = default);
}

public class CollectionsClient(HttpClient httpClient) : ICollectionsClient
{
    public async Task<List<CardCollectionDto>> GetCollectionsAsync(GetCollectionsRequest request, CancellationToken ct = default)
    {
        var url = $"/collections?Name={Uri.EscapeDataString(request.Name ?? string.Empty)}";
        return await httpClient.GetFromJsonAsync<List<CardCollectionDto>>(url, ct) ?? [];
    }

    public async Task<CardCollectionDto?> CreateCollectionAsync(CreateCollectionRequest request, CancellationToken ct = default)
    {
        var response = await httpClient.PostAsJsonAsync("/collections", request, ct);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CardCollectionDto>(ct);
        }
        return null;
    }

    public async Task AddCardToCollectionAsync(AddCardToCollectionRequest request, CancellationToken ct = default)
    {
        var url = $"/collections/{request.CollectionId}/cards/{request.CardId}";
        await httpClient.PostAsync(url, null, ct);
    }

    public async Task RemoveCardFromCollectionAsync(RemoveCardFromCollectionRequest request, CancellationToken ct = default)
    {
        var url = $"/collections/{request.CollectionId}/cards/{request.CardId}";
        await httpClient.DeleteAsync(url, ct);
    }
}
