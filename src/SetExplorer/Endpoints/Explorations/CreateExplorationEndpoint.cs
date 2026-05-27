using Microsoft.EntityFrameworkCore;

using SetExplorer.Client.Features.Collections;
using SetExplorer.Client.Features.Explorations;
using SetExplorer.Data;
using SetExplorer.Data.Explorations;

namespace SetExplorer.Endpoints.Explorations;

internal class CreateExplorationEndpoint(ExplorationService explorationService) : FastEndpoints.Endpoint<CreateExplorationRequest, ExplorationDto>
{
    public override void Configure()
    {
        Post("/explorations");
    }

    public override async Task HandleAsync(CreateExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await explorationService.CreateAsync(userId, req, ct);

        await result.Match(
            exploration => Send.OkAsync(exploration, ct),
            _ => Send.UnauthorizedAsync(ct)
        );
    }
}
