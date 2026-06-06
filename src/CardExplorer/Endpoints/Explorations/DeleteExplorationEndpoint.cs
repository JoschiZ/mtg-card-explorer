using CardExplorer.Client.Features.Explorations;

namespace CardExplorer.Endpoints.Explorations;

internal class DeleteExplorationEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<DeleteExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations");
    }

    public override async Task HandleAsync(DeleteExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.DeleteAsync(userId, req.Id, ct);

        await result.Match(
            exploration => Send.OkAsync(exploration, ct),
            _ => Send.NotFoundAsync(ct)
        );
    }
}
