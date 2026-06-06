using System.Net.Http.Json;
using CardExplorer.Client.Features.Collections;

namespace CardExplorer.Client.Features.Explorations;

public class ExplorationsClient(HttpClient httpClient) : IExplorationsClient
{
    public async Task<List<ExplorationSummaryDto>> GetExplorationsAsync(GetExplorationsRequest request, CancellationToken ct = default)
    {
        var query = request.Name is not null ? $"?Name={Uri.EscapeDataString(request.Name)}" : string.Empty;
        
        var url = $"explorations{query}";
        return await httpClient.GetFromJsonAsync<List<ExplorationSummaryDto>>(url, ct) ?? [];
    }

    public async Task<ExplorationDto?> GetExplorationByIdAsync(ExplorationId explorationId, CancellationToken ct = default)
    {
        var url = $"explorations/{explorationId}";
        return await httpClient.GetFromJsonAsync<ExplorationDto>(url, ct);
    }

    public async Task<ExplorationDto?> CreateExplorationAsync(CreateExplorationRequest request, CancellationToken ct = default)
    {
        var response = await httpClient.PostAsJsonAsync("explorations", request, ct);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ExplorationDto>(ct);
        }
        return null;
    }

    public async Task UpdateExplorationAsync(PatchExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"explorations/{request.Id}";
        await httpClient.PatchAsJsonAsync(url, request, ct);
    }

    public async Task AddCollectionToExplorationAsync(AddCollectionToExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"explorations/{request.ExplorationId}/collections/{request.CollectionId}";
        await httpClient.PostAsync(url, null, ct);
    }

    public async Task RemoveCollectionFromExplorationAsync(RemoveCollectionFromExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"explorations/{request.ExplorationId}/collections/{request.CollectionId}";
        await httpClient.DeleteAsync(url, ct);
    }

    public async Task AddSeenCardToExplorationAsync(AddSeenCardToExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"explorations/{request.ExplorationId}/seen-cards/{request.CardId}";
        await httpClient.PostAsync(url, null, ct);
    }

    public async Task RemoveSeenCardFromExplorationAsync(RemoveSeenCardFromExplorationRequest request, CancellationToken ct = default)
    {
        var url = $"explorations/{request.ExplorationId}/seen-cards/{request.CardId}";
        await httpClient.DeleteAsync(url, ct);
    }
}
