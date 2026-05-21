using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Data;
using CollectionId = SetExplorer.Client.Features.Collections.CollectionId;


namespace SetExplorer.Endpoints.Collections;

public class RemoveCardFromCollectionEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<RemoveCardFromCollectionRequest>
{
    public override void Configure()
    {
        Delete("/collections/{collectionId:guid}/cards/{cardId:guid}");
    }

    public override async Task HandleAsync(RemoveCardFromCollectionRequest req, CancellationToken ct)
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

        var card = collection.Cards.FirstOrDefault(c => c.Id == scryId);
        if (card != null)
        {
            collection.Cards.Remove(card);
            await db.SaveChangesAsync(ct);
        }
    }
}
