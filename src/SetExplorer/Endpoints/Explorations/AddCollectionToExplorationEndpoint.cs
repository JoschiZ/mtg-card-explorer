using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

public class AddCollectionToExplorationEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<AddCollectionToExplorationRequest>
{
    public override void Configure()
    {
        Post("/explorations/{explorationId:guid}/collections/{collectionId:guid}");
    }

    public override async Task HandleAsync(AddCollectionToExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var expId = ExplorationId.From(req.ExplorationId);
        var collId = CollectionId.From(req.CollectionId);

        var exploration = await db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations)
            .Include(e => e.CardCollections)
            .FirstOrDefaultAsync(e => e.Id == expId, ct);

        if (exploration == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var collection = await db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections)
            .FirstOrDefaultAsync(c => c.Id == collId, ct);

        if (collection == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (exploration.CardCollections.All(c => c.Id != collId))
        {
            exploration.CardCollections.Add(collection);
            await db.SaveChangesAsync(ct);
        }
    }
}
