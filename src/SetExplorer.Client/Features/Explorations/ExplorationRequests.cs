using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;

namespace SetExplorer.Client.Features.Explorations;

public class AddCollectionToExplorationRequest
{
    public required ExplorationId ExplorationId { get; init; }
    public required CollectionId CollectionId { get; init; }
}

public class AddSeenCardToExplorationRequest
{
    public required ExplorationId ExplorationId { get; init; }
    public required ScryfallCardId CardId { get; init; }
}

public class CreateExplorationRequest
{
    public required string Name { get; set; }
    public required string SearchString { get; set; }
}

public class GetExplorationByIdRequest
{
    public required ExplorationId Id { get; init; }
}

public class GetExplorationsRequest
{
    public string? Name { get; set; }
}

public class RemoveCollectionFromExplorationRequest
{
    public required ExplorationId ExplorationId { get; init; }
    public required CollectionId CollectionId { get; init; }
}

public class RemoveSeenCardFromExplorationRequest
{
    public required ExplorationId ExplorationId { get; init; }
    public required ScryfallCardId CardId { get; init; }
}

public sealed class PatchExplorationRequest
{
    public required ExplorationId Id { get; init; }
    public required string Name { get; set; }
    public required string SearchString { get; set; }
}
