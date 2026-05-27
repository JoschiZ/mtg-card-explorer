using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;

namespace SetExplorer.Client.Features.Explorations;

public record AddCollectionToExplorationRequest
{
    public ExplorationId ExplorationId { get; init; }
    public CollectionId CollectionId { get; init; }
}

public record AddSeenCardToExplorationRequest
{
    public ExplorationId ExplorationId { get; init; }
    public ScryfallCardId CardId { get; init; }
}

public record CreateExplorationRequest
{
    public required string Name { get; set; }
    public required string SearchString { get; set; }
}

public record GetExplorationsRequest
{
    public string? Name { get; set; }
}

public record RemoveCollectionFromExplorationRequest
{
    public ExplorationId ExplorationId { get; init; }
    public CollectionId CollectionId { get; init; }
}

public record RemoveSeenCardFromExplorationRequest
{
    public ExplorationId ExplorationId { get; init; }
    public CollectionId CardId { get; init; }
}

public sealed class PatchExplorationRequest
{
    public required ExplorationId Id { get; init; }
    public required string Name { get; set; }
    public required string SearchString { get; set; }
}
