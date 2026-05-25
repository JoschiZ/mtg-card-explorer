namespace SetExplorer.Client.Features.Collections;

public interface ICollectionsClient
{
    Task<CardCollectionDto[]> GetCollectionsAsync(GetCollectionsRequest request, CancellationToken ct = default);
    Task<CardCollectionDto?> CreateCollectionAsync(CreateCollectionRequest request, CancellationToken ct = default);
    Task AddCardToCollectionAsync(AddCardToCollectionRequest request, CancellationToken ct = default);
    Task RemoveCardFromCollectionAsync(RemoveCardFromCollectionRequest request, CancellationToken ct = default);
}