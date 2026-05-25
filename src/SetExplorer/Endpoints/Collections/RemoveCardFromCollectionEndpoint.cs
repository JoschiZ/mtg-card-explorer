using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core.Http;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;
using SetExplorer.Data;
using CollectionId = SetExplorer.Client.Features.Collections.CollectionId;


namespace SetExplorer.Endpoints.Collections;

internal class RemoveCardFromCollectionEndpoint : FastEndpoints.Endpoint<RemoveCardFromCollectionRequest>
{
    private readonly CardCollectionService _cardCollectionService;

    public RemoveCardFromCollectionEndpoint(CardCollectionService cardCollectionService)
    {
        _cardCollectionService = cardCollectionService;
    }

    public override void Configure()
    {
        Delete("/collections/{collectionId:guid}/cards/{cardId:guid}");
    }

    public override async Task HandleAsync(RemoveCardFromCollectionRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();
        var result = await _cardCollectionService.RemoveCardAsync(userId, req, ct);
        await result.Match(
            x => Send.OkAsync(cancellation: ct),
            x => Send.NotFoundAsync(ct)
        );

    }
}
