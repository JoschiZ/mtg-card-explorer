using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

public class RemoveSeenCardFromExplorationEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<RemoveSeenCardFromExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations/{explorationId:guid}/seen-cards/{cardId:guid}");
    }

    public override async Task HandleAsync(RemoveSeenCardFromExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var expId = ExplorationId.From(req.ExplorationId);
        var scryId = ScryfallCardId.From(req.CardId);

        var exploration = await db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations)
            .Include(e => e.SeenCards)
            .FirstOrDefaultAsync(e => e.Id == expId, ct);

        if (exploration == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var card = exploration.SeenCards.FirstOrDefault(c => c.Id == scryId);
        if (card != null)
        {
            exploration.SeenCards.Remove(card);
            await db.SaveChangesAsync(ct);
        }
    }
}
