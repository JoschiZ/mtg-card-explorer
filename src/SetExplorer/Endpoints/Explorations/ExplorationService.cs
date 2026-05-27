using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SetExplorer.Client.Core;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;
using SetExplorer.Data.Cards;
using SetExplorer.Data.Explorations;

namespace SetExplorer.Endpoints.Explorations;

internal sealed class ExplorationService
{
    private readonly ApplicationDbContext _db;

    public ExplorationService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<ExplorationDto>> GetAsync(UserId userId, GetExplorationsRequest request,
        CancellationToken cancellationToken)
    {
        var query = _db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations);

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(c => c.Name.Contains(request.Name));
        }

        var explorations = await query
            .Include(e => e.SeenCards)
            .Include(e => e.CardCollections)
            .ToListAsync(cancellationToken);
        return explorations.Select(MapToDto).ToList();
    }

    public async Task<OneOf<ExplorationDto, NotFound>> CreateAsync(UserId userId, CreateExplorationRequest request,
        CancellationToken cancellationToken)
    {
        var exploration = new Exploration
        {
            Name = request.Name,
            SearchString = request.SearchString,
            UserId = userId
        };

        var userEntity = await _db.Users.Include(u => u.Explorations)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (userEntity == null)
        {
            return new NotFound();
        }

        userEntity.Explorations.Add(exploration);
        await _db.SaveChangesAsync(cancellationToken);

        return MapToDto(exploration);
    }

    public async Task<OneOf<Success, NotFound>> UpdateAsync(UserId userId, PatchExplorationRequest request,
        CancellationToken cancellationToken)
    {
        var entry = await _db.Users
            .Where(x => x.Id == userId)
            .SelectMany(x => x.Explorations)
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entry is null)
        {
            return new NotFound();
        }

        entry.Name = request.Name;
        entry.SearchString = request.SearchString;

        await _db.SaveChangesAsync(cancellationToken);
        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> AddCollectionAsync(UserId userId,
        AddCollectionToExplorationRequest request, CancellationToken cancellationToken)
    {
        var exploration = await _db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations)
            .Include(e => e.CardCollections)
            .FirstOrDefaultAsync(e => e.Id == request.ExplorationId, cancellationToken);

        if (exploration == null)
        {
            return new NotFound();
        }

        var collection = await _db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections)
            .FirstOrDefaultAsync(c => c.Id == request.CollectionId, cancellationToken);

        if (collection == null)
        {
            return new NotFound();
        }

        if (exploration.CardCollections.Any(c => c.Id == request.CollectionId))
        {
            return new Success();
        }

        exploration.CardCollections.Add(collection);
        await _db.SaveChangesAsync(cancellationToken);

        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> RemoveCollectionAsync(UserId userId,
        RemoveCollectionFromExplorationRequest request, CancellationToken cancellationToken)
    {
        var exploration = await _db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations)
            .Include(e => e.CardCollections)
            .FirstOrDefaultAsync(e => e.Id == request.ExplorationId, cancellationToken);

        if (exploration == null)
        {
            return new NotFound();
        }

        var collection = exploration.CardCollections.FirstOrDefault(c => c.Id == request.CollectionId);
        if (collection != null)
        {
            exploration.CardCollections.Remove(collection);
            await _db.SaveChangesAsync(cancellationToken);
        }

        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> AddSeenCardAsync(UserId userId, AddSeenCardToExplorationRequest request,
        CancellationToken cancellationToken)
    {
        var exploration = await _db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations)
            .Include(e => e.SeenCards)
            .FirstOrDefaultAsync(e => e.Id == request.ExplorationId, cancellationToken);

        if (exploration == null)
        {
            return new NotFound();
        }

        if (exploration.SeenCards.All(c => c.Id != request.CardId))
        {
            var card = await _db.Set<Card>().FindAsync([request.CardId], cancellationToken);
            if (card == null)
            {
                card = new Card { Id = request.CardId };
                _db.Set<Card>().Add(card);
            }

            exploration.SeenCards.Add(card);
            await _db.SaveChangesAsync(cancellationToken);
        }

        return new Success();
    }

    public async Task<OneOf<Success, NotFound>> RemoveSeenCardAsync(UserId userId,
        RemoveSeenCardFromExplorationRequest request, CancellationToken cancellationToken)
    {
        var exploration = await _db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations)
            .Include(e => e.SeenCards)
            .FirstOrDefaultAsync(e => e.Id == request.ExplorationId, cancellationToken);

        if (exploration == null)
        {
            return new NotFound();
        }

        var scryId = ScryfallCardId.From(request.CardId.Value);
        var card = exploration.SeenCards.FirstOrDefault(c => c.Id == scryId);
        if (card != null)
        {
            exploration.SeenCards.Remove(card);
            await _db.SaveChangesAsync(cancellationToken);
        }

        return new Success();
    }

    private static ExplorationDto MapToDto(Exploration exploration)
    {
        return new ExplorationDto
        {
            Name = exploration.Name,
            SearchString = exploration.SearchString,
            UserId = exploration.UserId,
            SeenCards = new ObservableCollection<ScryfallCardId>(exploration.SeenCards.Select(c => c.Id)),
            CardCollections = new ObservableCollection<CardCollectionDto>(exploration.CardCollections.Select(c =>
                new CardCollectionDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    UserId = c.UserId,
                    Cards = new ObservableCollection<ScryfallCardId>(c.Cards.Select(card => card.Id))
                }))
        };
    }
}