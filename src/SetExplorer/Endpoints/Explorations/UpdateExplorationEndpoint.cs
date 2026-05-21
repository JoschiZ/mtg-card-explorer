using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

public class UpdateExplorationEndpoint : Endpoint<PatchExplorationRequest>
{
    private readonly ApplicationDbContext _context;

    public UpdateExplorationEndpoint(ApplicationDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Patch("explorations/{explorationId}");
    }

    public override async Task HandleAsync(PatchExplorationRequest req, CancellationToken ct)
    {
        var userId =  this.GetUserId();

        var entry = await _context
            .Users
            .Where(x => x.Id == userId)
            .SelectMany(x => x.Explorations)
            .Where(x => x.Id == req.Id)
            .FirstOrDefaultAsync(cancellationToken: ct);

        if (entry is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        entry.Name = req.Name;
        entry.SearchString = req.SearchString;

        await _context.SaveChangesAsync(ct);
    }
}