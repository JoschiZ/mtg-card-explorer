using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;



namespace SetExplorer.Endpoints.Explorations;

internal class RemoveSeenCardFromExplorationEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<RemoveSeenCardFromExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations/{explorationId:guid}/seen-cards/{cardId:guid}");
    }

    public override async Task HandleAsync(RemoveSeenCardFromExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.RemoveSeenCardAsync(userId, req, ct);
        await result.Match(_ => Send.NoContentAsync(ct), _ => Send.NotFoundAsync(ct));
    }
}
