using Microsoft.EntityFrameworkCore;

using SetExplorer.Client.Features.Collections;
using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;
using SetExplorer.Data.Explorations;

namespace SetExplorer.Endpoints.Explorations;

internal class GetExplorationsEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<GetExplorationsRequest, List<ExplorationDto>>
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
