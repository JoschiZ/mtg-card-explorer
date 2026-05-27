using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

internal class AddCollectionToExplorationEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<AddCollectionToExplorationRequest>
{
    public override void Configure()
    {
        Post("/explorations/{explorationId:guid}/collections/{collectionId:guid}");
    }

    public override async Task HandleAsync(AddCollectionToExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.AddCollectionAsync(userId, req, ct);
        await result.Match(x => Send.OkAsync(x, ct), x => Send.NotFoundAsync(ct));
    }
}
