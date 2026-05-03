using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Collections;

public record CreateCollectionRequest
{
    public required string Name { get; init; }
}

public class CreateCollectionEndpoint(ApplicationDbContext db) : FastEndpoints.Endpoint<CreateCollectionRequest, CardCollection>
{
    public override void Configure()
    {
        Post("/collections");
    }

    public override async Task HandleAsync(CreateCollectionRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();

        var collection = new CardCollection
        {
            Name = req.Name,
            UserId = userId
        };

        var userEntity = await db.Users.Include(u => u.CardCollections).FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (userEntity == null)
        {
            await SendAsync(null, 401, ct);
            return;
        }
        
        userEntity.CardCollections.Add(collection);
        await db.SaveChangesAsync(ct);

        await SendCreatedAtAsync<CreateCollectionEndpoint>(new { collection.Id }, collection, cancellation: ct);
    }
}
