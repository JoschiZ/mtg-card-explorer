
using SetExplorer.Client.Features.Cards;

namespace SetExplorer.Client.Features.Collections;

public record AddCardToCollectionRequest
{
    public required CollectionId CollectionId { get; init; }
    public required ScryfallCardId CardId { get; init; }
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
    public CollectionId CollectionId { get; init; }
    public ScryfallCardId CardId { get; init; }
}
