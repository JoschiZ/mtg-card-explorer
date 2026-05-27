using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SetExplorer.Client.Core;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Data;
using SetExplorer.Data.Cards;
using SetExplorer.Data.Collections;

namespace SetExplorer.Endpoints.Collections;

internal class CardCollectionService(ApplicationDbContext db)
{
    public async Task<OneOf<Success, NotFound>> RetrieveAndAddCardToCollection(UserId userId, AddCardToCollectionRequest request,  CancellationToken ct)
    {
        var collection = await db
            .Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections)
            .Include(c => c.Cards)
            .FirstOrDefaultAsync(c => c.Id == request.CollectionId, ct);

        if (collection == null)
        {
            return new NotFound();
        }

        if (collection.Cards.Any(c => c.Id == request.CardId))
        {
            return new Success();
        }
        
        var card = await db
            .Cards
            .Where(x => x.Id == request.CardId)
            .FirstOrDefaultAsync(ct);
        if (card == null)
        {
            card = new Card { Id = request.CardId };
            db.Cards.Add(card);
        }
        collection.Cards.Add(card);
        await db.SaveChangesAsync(ct);

        return new Success();
    }

    public async Task<OneOf<CardCollectionDto>> CreateNewCollection(UserId userId,
        CreateCollectionRequest request, CancellationToken ct)
    {
        var collection = new CardCollection
        {
            Name = request.Name,
            UserId = userId
        };

        db.Add(collection);

        await db.SaveChangesAsync(ct);

        return collection.ToDto();
    }

    public async Task<OneOf<CardCollectionDto[]>> SearchCollectionsAsync(UserId userId,
        GetCollectionsRequest request, CancellationToken ct)
    {
        return await db
            .Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections)
            .Where(x => request.Name == null || x.Name == request.Name)
            .ProjectToDto()
            .ToArrayAsync(ct);
    }

    public async Task<OneOf<Success, NotFound>> RemoveCardAsync(UserId userId, RemoveCardFromCollectionRequest request, CancellationToken ct)
    {
        var collection = await db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections)
            .Include(c => c.Cards)
            .FirstOrDefaultAsync(c => c.Id == request.CollectionId, ct);

        if (collection == null)
        {
            return new NotFound();
        }

        var card = collection.Cards.FirstOrDefault(c => c.Id == request .CardId);
        if (card == null)
        {
            return new NotFound();
        }
        collection.Cards.Remove(card);
        await db.SaveChangesAsync(ct);
        return new Success();
    }
}