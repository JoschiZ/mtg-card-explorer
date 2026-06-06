using Microsoft.EntityFrameworkCore;

using CardExplorer.Client.Features.Collections;
using CardExplorer.Data;
using CardExplorer.Data.Collections;

namespace CardExplorer.Endpoints.Collections;

internal class CreateCollectionEndpoint : FastEndpoints.Endpoint<CreateCollectionRequest, CardCollectionDto>
{
    private readonly CardCollectionService _cardCollectionService;

    public CreateCollectionEndpoint(CardCollectionService cardCollectionService)
    {
        _cardCollectionService = cardCollectionService;
    }

    public override void Configure()
    {
        Post("/collections");
    }

    public override async Task HandleAsync(CreateCollectionRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await _cardCollectionService.CreateNewCollection(userId, req, ct);
        await result.Match(x => Send.OkAsync(x, ct));
    }
}
