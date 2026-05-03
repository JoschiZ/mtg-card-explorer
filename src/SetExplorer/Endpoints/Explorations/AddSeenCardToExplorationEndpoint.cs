using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

public record AddSeenCardToExplorationRequest
{
    public Guid ExplorationId { get; init; }
    public Guid CardId { get; init; }
}

public class AddSeenCardToExplorationEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<AddSeenCardToExplorationRequest>
{
    public override void Configure()
    {
        Post("/explorations/{explorationId:guid}/seen-cards/{cardId:guid}");
    }

    public override async Task HandleAsync(AddSeenCardToExplorationRequest req, CancellationToken ct)
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
            await SendNotFoundAsync(ct);
            return;
        }

        if (exploration.SeenCards.All(c => c.Id != scryId))
        {
            var card = await db.Set<Card>().FindAsync([scryId], ct);
            if (card == null)
            {
                card = new Card { Id = scryId };
                db.Set<Card>().Add(card);
            }
            exploration.SeenCards.Add(card);
            await db.SaveChangesAsync(ct);
        }

        await SendOkAsync(ct);
    }
}
