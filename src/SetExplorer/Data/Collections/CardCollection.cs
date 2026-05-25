using System.Collections.ObjectModel;
using SetExplorer.Client.Core;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Data.Cards;
using SetExplorer.Data.Explorations;

namespace SetExplorer.Data.Collections;

public class CardCollection
{
    public CollectionId Id { get; private set; } = CollectionId.FromNewVersion7Guid();
    public required string Name { get; init; }
    
    public List<Card> Cards { get; init; } = [];
    public List<Exploration> Explorations { get; init; } = [];
    public required UserId UserId { get; init; }

    public CardCollectionDto ToDto()
    {
        return new CardCollectionDto
        {
            Name = Name,
            Id = Id,
            Cards = new ObservableCollection<ScryfallCardId>(Cards.Select(x => x.Id)),
            UserId = UserId
        };
    }
}

public static class CardCollectionExtensions
{
    public static IQueryable<CardCollectionDto> ProjectToDto(this IQueryable<CardCollection> collections)
        => collections
            .Select(x => new CardCollectionDto
            {
                Name = x.Name,
                Id = x.Id,
                Cards = new ObservableCollection<ScryfallCardId>(x.Cards.Select(card => card.Id)),
                UserId = x.UserId
            });
}

