using Microsoft.EntityFrameworkCore;

using SetExplorer.Client.Features.Collections;
using SetExplorer.Data;
using SetExplorer.Data.Collections;

namespace SetExplorer.Endpoints.Collections;

internal class GetCollectionsEndpoint : FastEndpoints.Endpoint<GetCollectionsRequest, CardCollectionDto[]>
{
    private readonly CardCollectionService _cardCollectionService;

    public GetCollectionsEndpoint(CardCollectionService cardCollectionService)
    {
        _cardCollectionService = cardCollectionService;
    }

    public override void Configure()
    {
        Get("/collections");
    }

    public override async Task HandleAsync(GetCollectionsRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await _cardCollectionService.SearchCollectionsAsync(userId, req, ct);
        await result.Match(x => Send.OkAsync(x, ct));
    }
}
