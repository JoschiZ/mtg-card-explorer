using System.Collections.ObjectModel;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Client.Core.Explorations;

namespace SetExplorer.Client.Core.Cards;

public class Card
{
    public required ScryfallCardId Id { get; init; }
    
    public List<CardCollection> Collections { get; init; } = [];
    public List<Exploration> Explorations { get; init; } = [];
}