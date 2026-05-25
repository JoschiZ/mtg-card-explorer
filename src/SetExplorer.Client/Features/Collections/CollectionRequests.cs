
namespace SetExplorer.Client.Features.Collections;

public record AddCardToCollectionRequest
{
    public required Guid CollectionId { get; init; }
    public required Guid CardId { get; init; }
}

public record CreateCollectionRequest
{
    public required string Name { get; set; }
}

public record GetCollectionsRequest
{
    public string? Name { get; set; }
}

public record RemoveCardFromCollectionRequest
{
    public Guid CollectionId { get; init; }
    public Guid CardId { get; init; }
}
