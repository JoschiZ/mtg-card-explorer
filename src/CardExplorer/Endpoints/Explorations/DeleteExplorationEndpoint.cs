using FastEndpoints;
using CardExplorer.Client.Features.Explorations;

namespace CardExplorer.Endpoints.Explorations;

internal class DeleteExplorationEndpoint(ExplorationService explorationService) : Endpoint<DeleteExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations/{explorationId}");
        Description(x => x.Accepts<DeleteExplorationRequest>());
    }

    public override async Task HandleAsync(DeleteExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.DeleteAsync(userId, req.ExplorationId, ct);

        await result.Match(
            exploration => Send.OkAsync(exploration, ct),
            _ => Send.NotFoundAsync(ct)
        );
    }
}
