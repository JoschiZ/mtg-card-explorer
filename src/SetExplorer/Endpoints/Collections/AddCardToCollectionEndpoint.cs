
using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Data;
using SetExplorer.Data.Cards;

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

        var collection = await db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections)
            .Include(c => c.Cards)
            .FirstOrDefaultAsync(c => c.Id == collId, ct);

        if (collection == null)
        {
            await Send.NotFoundAsync(ct);
            return;
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
    }
}
