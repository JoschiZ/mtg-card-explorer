namespace SetExplorer.Client.Features.Explorations;

public record AddCollectionToExplorationRequest
{
    public Guid ExplorationId { get; init; }
    public Guid CollectionId { get; init; }
}

public record AddSeenCardToExplorationRequest
{
    public Guid ExplorationId { get; init; }
    public Guid CardId { get; init; }
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
    public Guid ExplorationId { get; init; }
    public Guid CollectionId { get; init; }
}

public record RemoveSeenCardFromExplorationRequest
{
    public Guid ExplorationId { get; init; }
    public Guid CardId { get; init; }
}

public sealed class PatchExplorationRequest
{
    public required ExplorationId Id { get; init; }
    public required string Name { get; set; }
    public required string SearchString { get; set; }
}
