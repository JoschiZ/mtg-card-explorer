
using FastEndpoints;
using CardExplorer.Client.Features.Explorations;

namespace CardExplorer.Endpoints.Explorations;

internal class AddCollectionToExplorationEndpoint(ExplorationService explorationService) : Endpoint<AddCollectionToExplorationRequest>
{
    public override void Configure()
    {
        Post("/explorations/{explorationId:guid}/collections/{collectionId:guid}");
        
        Description(x => x.Accepts<AddCollectionToExplorationRequest>());
    }

    public override async Task HandleAsync(AddCollectionToExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.AddCollectionAsync(userId, req, ct);
        await result.Match(x => Send.OkAsync(x, ct), x => Send.NotFoundAsync(ct));
    }
}
