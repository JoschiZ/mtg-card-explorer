using Microsoft.EntityFrameworkCore;

using CardExplorer.Client.Features.Collections;
using CardExplorer.Client.Features.Explorations;
using CardExplorer.Data;
using CardExplorer.Data.Explorations;

namespace CardExplorer.Endpoints.Explorations;

internal class GetExplorationsEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<GetExplorationsRequest, List<ExplorationSummaryDto>>
{
    public override void Configure()
    {
        Get("/explorations");
    }

    public override async Task HandleAsync(GetExplorationsRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var explorations = await explorationService.GetAsync(userId, req, ct);
        await Send.OkAsync(explorations, ct);
    }
}
