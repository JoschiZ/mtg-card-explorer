using FastEndpoints;
using Microsoft.EntityFrameworkCore;

using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

internal class UpdateExplorationEndpoint(ExplorationService explorationService) : Endpoint<PatchExplorationRequest>
{
    public override void Configure()
    {
        Patch("explorations/{explorationId}");
    }

    public override async Task HandleAsync(PatchExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.UpdateAsync(userId, req, ct);
        await result.Match(_ => Send.NoContentAsync(ct), _ => Send.NotFoundAsync(ct));
    }
}