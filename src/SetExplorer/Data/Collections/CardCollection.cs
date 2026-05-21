using SetExplorer.Client.Core;

using SetExplorer.Data.Cards;
using SetExplorer.Data.Explorations;
using CollectionId = SetExplorer.Client.Features.Collections.CollectionId;

namespace SetExplorer.Data.Collections;

public class CardCollection
{
    public CollectionId Id { get; private set; } = CollectionId.FromNewVersion7Guid();
    public required string Name { get; init; }
    
    public List<Card> Cards { get; init; } = [];
    public List<Exploration> Explorations { get; init; } = [];
    public required UserId UserId { get; init; }
}

