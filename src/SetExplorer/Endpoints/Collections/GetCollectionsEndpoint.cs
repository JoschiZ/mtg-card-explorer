using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Collections;

public record GetCollectionsRequest
{
    public string? Name { get; init; }
}

public class GetCollectionsEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<GetCollectionsRequest, List<CardCollection>>
{
    public override void Configure()
    {
        Get("/collections");
    }

    public override async Task HandleAsync(GetCollectionsRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var query = db.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.CardCollections);

        if (!string.IsNullOrWhiteSpace(req.Name))
        {
            query = query.Where(c => c.Name.Contains(req.Name));
        }

        var collections = await query.ToListAsync(ct);
        await SendOkAsync(collections, ct);
    }
}
