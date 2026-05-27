using FastEndpoints;
using SetExplorer.Client.Features.Explorations;


namespace SetExplorer.Endpoints.Explorations;

internal class RemoveSeenCardFromExplorationEndpoint(ExplorationService explorationService) : Endpoint<RemoveSeenCardFromExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations/{explorationId:guid}/seen-cards/{cardId:guid}");
        Description(x => x.Accepts<RemoveSeenCardFromExplorationRequest>());
    }

    public override async Task HandleAsync(RemoveSeenCardFromExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.RemoveSeenCardAsync(userId, req, ct);
        await result.Match(_ => Send.NoContentAsync(ct), _ => Send.NotFoundAsync(ct));
    }
}
