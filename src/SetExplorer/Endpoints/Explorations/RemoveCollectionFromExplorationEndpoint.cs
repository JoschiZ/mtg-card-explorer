using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

public record RemoveCollectionFromExplorationRequest
{
    public Guid ExplorationId { get; init; }
    public Guid CollectionId { get; init; }
}

public class RemoveCollectionFromExplorationEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<RemoveCollectionFromExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations/{explorationId:guid}/collections/{collectionId:guid}");
    }

    public override async Task HandleAsync(RemoveCollectionFromExplorationRequest req, CancellationToken ct)
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

        var collection = exploration.CardCollections.FirstOrDefault(c => c.Id == collId);
        if (collection != null)
        {
            exploration.CardCollections.Remove(collection);
            await db.SaveChangesAsync(ct);
        }
    }
}
