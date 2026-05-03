using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Explorations;

public record GetExplorationsRequest
{
    public string? Name { get; init; }
}

public class GetExplorationsEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<GetExplorationsRequest, List<Exploration>>
{
    public override void Configure()
    {
        Get("/explorations");
    }

    public override async Task HandleAsync(GetExplorationsRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var query = db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Explorations);

        if (!string.IsNullOrWhiteSpace(req.Name))
        {
            query = query.Where(c => c.Name.Contains(req.Name));
        }

        var explorations = await query.ToListAsync(ct);
        await SendOkAsync(explorations, ct);
    }
}
