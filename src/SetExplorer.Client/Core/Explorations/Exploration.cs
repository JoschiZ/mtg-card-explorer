using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Client.Core.Scryfall.Models;
using Vogen;

namespace SetExplorer.Client.Core.Explorations;

public class Exploration
{
    public ExplorationId Id  { get; private set; } = ExplorationId.FromNewVersion7Guid(); 
    public required string Name { get; set; }
    public required string SearchString  { get; set; }

    public List<ScryfallCard> SeenCards { get; set; } = [];
    public List<CardCollection>  CardCollections { get; set; } = [];
    public required UserId UserId { get; init; }
}

[ValueObject<Guid>]
public readonly partial struct ExplorationId;