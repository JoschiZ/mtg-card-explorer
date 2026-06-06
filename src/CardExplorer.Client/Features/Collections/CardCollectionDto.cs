using System.Collections.ObjectModel;
using CardExplorer.Client.Core;
using CardExplorer.Client.Features.Cards;
using CardExplorer.Client.Features.Explorations;

namespace CardExplorer.Client.Features.Collections;

public sealed class CardCollectionDto
{
    public required string Name { get; init; }
    public required CollectionId Id { get; init; }
    public required ObservableCollection<ScryfallCardId> Cards { get; init; }
    public required UserId UserId { get; init; }
}

public sealed class ExplorationSummaryDto
{
    public required ExplorationId Id { get; init; }
    public required string Name { get; init; }
    public required string SearchString  { get; set; }
    public required UserId UserId { get; init; }
}

public sealed class ExplorationDto
{
    public required ExplorationId Id { get; init; }
    public required string Name { get; init; }
    public required string SearchString  { get; set; }
    public required ObservableCollection<ScryfallCardId> SeenCards { get; init; }
    public required UserId UserId { get; init; }
    public required ObservableCollection<CardCollectionDto> CardCollections { get; init; }
}