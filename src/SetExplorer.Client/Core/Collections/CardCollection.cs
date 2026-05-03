using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Client.Core.Scryfall.Models;
using Vogen;

namespace SetExplorer.Client.Core.Collections;

public class CardCollection
{
    public CollectionId Id { get; private set; } = CollectionId.FromNewVersion7Guid();
    public required string Name { get; init; }
    
    public List<Card> Cards { get; init; } = [];
    public List<Exploration> Explorations { get; init; } = [];
    public required UserId UserId { get; init; }
}

[ValueObject<Guid>]
public readonly partial struct CollectionId;

