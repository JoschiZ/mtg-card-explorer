
using CardExplorer.Client.Features.Cards;
using CardExplorer.Data.Collections;
using CardExplorer.Data.Explorations;


namespace CardExplorer.Data.Cards;

public class Card
{
    public required ScryfallCardId Id { get; init; }
    
    public List<CardCollection> Collections { get; init; } = [];
    public List<Exploration> Explorations { get; init; } = [];
}
