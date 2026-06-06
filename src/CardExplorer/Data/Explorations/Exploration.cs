using CardExplorer.Client.Core;
using CardExplorer.Client.Features.Explorations;
using CardExplorer.Data.Cards;
using CardExplorer.Data.Collections;


namespace CardExplorer.Data.Explorations;

public class Exploration
{
    public ExplorationId Id  { get; private set; } = ExplorationId.FromNewVersion7Guid(); 
    public required string Name { get; set; }
    public required string SearchString  { get; set; }

    public List<Card> SeenCards { get; set; } = [];
    public List<CardCollection>  CardCollections { get; set; } = [];
    public required UserId UserId { get; init; }
}