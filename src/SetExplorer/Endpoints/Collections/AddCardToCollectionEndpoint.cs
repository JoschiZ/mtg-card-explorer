
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using SetExplorer.Client.Core;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Data;
using SetExplorer.Data.Cards;
using CollectionId = SetExplorer.Client.Features.Collections.CollectionId;


namespace SetExplorer.Endpoints.Collections;

public class AddCardToCollectionEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<AddCardToCollectionRequest>
{
    public override void Configure()
    {
        Post("/collections/{collectionId:guid}/cards/{cardId:guid}");
    }

    public override async Task HandleAsync(AddCardToCollectionRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var collId = CollectionId.From(req.CollectionId);
        var scryId = ScryfallCardId.From(req.CardId);

        var result = await RetrieveAndAddCardToCollection(ct, userId, collId, scryId);

        await result.Match(
            _ => Send.NotFoundAsync(ct),
            _ => Send.OkAsync(cancellation: ct)
        );
    }

    private async Task<OneOf<NotFound, Success>> RetrieveAndAddCardToCollection(CancellationToken ct, UserId userId, CollectionId collId, ScryfallCardId scryId)
    {
        var collection = await db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections)
            .Include(c => c.Cards)
            .FirstOrDefaultAsync(c => c.Id == collId, ct);

        if (collection == null)
        {
            return new NotFound();
        }

        if (collection.Cards.All(c => c.Id != scryId))
        {
            var card = await db.Set<Card>().FindAsync([scryId], ct);
            if (card == null)
            {
                card = new Card { Id = scryId };
                db.Set<Card>().Add(card);
            }
            collection.Cards.Add(card);
            await db.SaveChangesAsync(ct);
        }

        return new Success();
    }
}
