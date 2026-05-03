using SetExplorer.Client.Core.Cards;
using SetExplorer.Data.Collections;
using SetExplorer.Data.Explorations;

namespace SetExplorer.Data.Cards;

public class Card
{
    public required ScryfallCardId Id { get; init; }
    
    public List<CardCollection> Collections { get; init; } = [];
    public List<Exploration> Explorations { get; init; } = [];
}
