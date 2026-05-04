using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Explorations;
using SetExplorer.Data;
using SetExplorer.Data.Explorations;

namespace SetExplorer.Endpoints.Explorations;

public record CreateExplorationRequest
{
    public required string Name { get; init; }
    public required string SearchString { get; init; }
}

public class CreateExplorationEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<CreateExplorationRequest, Exploration>
{
    public override void Configure()
    {
        Post("/explorations");
    }

    public override async Task HandleAsync(CreateExplorationRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var exploration = new Exploration
        {
            Name = req.Name,
            SearchString = req.SearchString,
            UserId = userId
        };

        var userEntity = await db.Users.Include(u => u.Explorations).FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (userEntity == null)
        {
            await Send.ResponseAsync(null, 401, ct);
            return;
        }
        
        userEntity.Explorations.Add(exploration);
        await db.SaveChangesAsync(ct);

        await Send.CreatedAtAsync<CreateExplorationEndpoint>(new { exploration.Id }, exploration, cancellation: ct);
    }
}
