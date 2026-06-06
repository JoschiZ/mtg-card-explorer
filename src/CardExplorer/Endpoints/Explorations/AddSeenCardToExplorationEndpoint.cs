using FastEndpoints;
using CardExplorer.Client.Features.Explorations;


namespace CardExplorer.Endpoints.Explorations;

internal class AddSeenCardToExplorationEndpoint(ExplorationService explorationService) : Endpoint<AddSeenCardToExplorationRequest>
{
    public override void Configure()
    {
        Post("/explorations/{explorationId:guid}/seen-cards/{cardId:guid}");
        
        Description(x => x.Accepts<AddSeenCardToExplorationRequest>());
    }

    public override async Task HandleAsync(AddSeenCardToExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.AddSeenCardAsync(userId, req, ct);
        await result.Match(x => Send.OkAsync(x, ct), x => Send.NotFoundAsync(ct));
    }
}
