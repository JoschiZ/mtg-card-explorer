using SetExplorer.Client.Features.Collections;

namespace SetExplorer.Client.Features.Explorations;

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