using Microsoft.EntityFrameworkCore;


using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;
using CollectionId = SetExplorer.Client.Features.Collections.CollectionId;


namespace SetExplorer.Endpoints.Explorations;

internal class RemoveCollectionFromExplorationEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<RemoveCollectionFromExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations/{explorationId:guid}/collections/{collectionId:guid}");
    }

    public override async Task HandleAsync(RemoveCollectionFromExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.RemoveCollectionAsync(userId, req, ct);
        await result.Match(x => Send.NoContentAsync(ct), _ => Send.NotFoundAsync(ct));
    }
}
