using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;
using SetExplorer.Data.Cards;



namespace SetExplorer.Endpoints.Explorations;

internal class AddSeenCardToExplorationEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<AddSeenCardToExplorationRequest>
{
    public override void Configure()
    {
        Post("/explorations/{explorationId:guid}/seen-cards/{cardId:guid}");
    }

    public override async Task HandleAsync(AddSeenCardToExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.AddSeenCardAsync(userId, req, ct);
        await result.Match(x => Send.OkAsync(x, ct), x => Send.NotFoundAsync(ct));
    }
}
