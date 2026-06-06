using CardExplorer.Client.Features.Collections;
using CardExplorer.Client.Features.Explorations;
using CardExplorer.Data;

namespace CardExplorer.Endpoints.Explorations;

internal class GetExplorationByIdEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<GetExplorationByIdRequest, ExplorationDto>
{
    public override void Configure()
    {
        Get("/explorations/{Id}");
    }

    public override async Task HandleAsync(GetExplorationByIdRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.GetByIdAsync(userId, req.Id, ct);
        
        await result.Match(
            exploration => Send.OkAsync(exploration, ct),
            notFound => Send.NotFoundAsync(ct)
        );
    }
}
