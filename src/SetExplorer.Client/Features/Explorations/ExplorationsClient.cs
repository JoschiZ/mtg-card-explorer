using System.Net.Http.Json;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Client.Features.Explorations;

namespace SetExplorer.Client.Core.Http;

public interface IExplorationsClient
{
    Task<List<ExplorationDto>> GetExplorationsAsync(GetExplorationsRequest request, CancellationToken ct = default);
    Task<ExplorationDto?> CreateExplorationAsync(CreateExplorationRequest request, CancellationToken ct = default);
    Task UpdateExplorationAsync(PatchExplorationRequest request, CancellationToken ct = default);
    Task AddCollectionToExplorationAsync(AddCollectionToExplorationRequest request, CancellationToken ct = default);
    Task RemoveCollectionFromExplorationAsync(RemoveCollectionFromExplorationRequest request, CancellationToken ct = default);
    Task AddSeenCardToExplorationAsync(AddSeenCardToExplorationRequest request, CancellationToken ct = default);
    Task RemoveSeenCardFromExplorationAsync(RemoveSeenCardFromExplorationRequest request, CancellationToken ct = default);
}

public class ExplorationsClient(HttpClient httpClient) : IExplorationsClient
{
    public async Task<List<ExplorationDto>> GetExplorationsAsync(GetExplorationsRequest request, CancellationToken ct = default)
    {
        var url = $"/explorations?Name={Uri.EscapeDataString(request.Name ?? string.Empty)}";
        return await httpClient.GetFromJsonAsync<List<ExplorationDto>>(url, ct) ?? [];
    }

    public async Task<ExplorationDto?> CreateExplorationAsync(CreateExplorationRequest request, CancellationToken ct = default)
    {
        var response = await httpClient.PostAsJsonAsync("/explorations", request, ct);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ExplorationDto>(ct);
        }
        return null;
    }

    public async Task UpdateExplorationAsync(PatchExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"/explorations/{request.Id}";
        await httpClient.PatchAsJsonAsync(url, request, ct);
    }

    public async Task AddCollectionToExplorationAsync(AddCollectionToExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"/explorations/{request.ExplorationId}/collections/{request.CollectionId}";
        await httpClient.PostAsync(url, null, ct);
    }

    public async Task RemoveCollectionFromExplorationAsync(RemoveCollectionFromExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"/explorations/{request.ExplorationId}/collections/{request.CollectionId}";
        await httpClient.DeleteAsync(url, ct);
    }

    public async Task AddSeenCardToExplorationAsync(AddSeenCardToExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"/explorations/{request.ExplorationId}/seen-cards/{request.CardId}";
        await httpClient.PostAsync(url, null, ct);
    }

    public async Task RemoveSeenCardFromExplorationAsync(RemoveSeenCardFromExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"/explorations/{request.ExplorationId}/seen-cards/{request.CardId}";
        await httpClient.DeleteAsync(url, ct);
    }
}
