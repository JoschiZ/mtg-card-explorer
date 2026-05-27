using SetExplorer.Client.Features.Explorations;
using FastEndpoints;


namespace SetExplorer.Endpoints.Explorations;

internal class RemoveCollectionFromExplorationEndpoint(ExplorationService explorationService) : Endpoint<RemoveCollectionFromExplorationRequest>
{
    public override void Configure()
    {
        Delete("/explorations/{explorationId:guid}/collections/{collectionId:guid}");
        Description(x => x.Accepts<RemoveCollectionFromExplorationRequest>());
    }

    public override async Task HandleAsync(RemoveCollectionFromExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.RemoveCollectionAsync(userId, req, ct);
        await result.Match(x => Send.NoContentAsync(ct), _ => Send.NotFoundAsync(ct));
    }
}
