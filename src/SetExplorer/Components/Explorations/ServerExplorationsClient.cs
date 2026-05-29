using Microsoft.AspNetCore.Components.Authorization;
using SetExplorer.Client.Core;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Client.Features.Explorations;
using SetExplorer.Endpoints.Explorations;

namespace SetExplorer.Components.Explorations;

internal class ServerExplorationsClient(
    ExplorationService explorationService,
    AuthenticationStateProvider authenticationStateProvider)
    : IExplorationsClient
{
    public async Task<List<ExplorationSummaryDto>> GetExplorationsAsync(GetExplorationsRequest request,
        CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        return await explorationService.GetAsync(userId, request, ct);
    }

    public async Task<ExplorationDto?> GetExplorationByIdAsync(ExplorationId explorationId, CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        var result = await explorationService.GetByIdAsync(userId, explorationId, ct);
        return result.Match(ExplorationDto? (x) => x, _ => null);
    }

    public async Task<ExplorationDto?> CreateExplorationAsync(CreateExplorationRequest request,
        CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        var result = await explorationService.CreateAsync(userId, request, ct);
        return result.Match(ExplorationDto? (x) => x, _ => null);
    }

    public async Task UpdateExplorationAsync(PatchExplorationRequest request, CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        await explorationService.UpdateAsync(userId, request, ct);
    }

    public async Task AddCollectionToExplorationAsync(AddCollectionToExplorationRequest request,
        CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        await explorationService.AddCollectionAsync(userId, request, ct);
    }

    public async Task RemoveCollectionFromExplorationAsync(RemoveCollectionFromExplorationRequest request,
        CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        await explorationService.RemoveCollectionAsync(userId, request, ct);
    }

    public async Task AddSeenCardToExplorationAsync(AddSeenCardToExplorationRequest request,
        CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        await explorationService.AddSeenCardAsync(userId, request, ct);
    }

    public async Task RemoveSeenCardFromExplorationAsync(RemoveSeenCardFromExplorationRequest request,
        CancellationToken ct = default)
    {
        var userId = await authenticationStateProvider.GetUserIdAsync();
        await explorationService.RemoveSeenCardAsync(userId, request, ct);
    }
}
