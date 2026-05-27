
using SetExplorer.Client.Features.Cards;

namespace SetExplorer.Client.Features.Collections;

public class AddCardToCollectionRequest
{
    public required CollectionId CollectionId { get; init; }
    public required ScryfallCardId CardId { get; init; }
}

public class CreateCollectionRequest
{
    public required string Name { get; set; }
}

public class GetCollectionsRequest
{
    public string? Name { get; set; }
}

public class RemoveCardFromCollectionRequest
{
    public required CollectionId CollectionId { get; init; }
    public required ScryfallCardId CardId { get; init; }
}
