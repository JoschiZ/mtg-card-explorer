

using FastEndpoints;
using SetExplorer.Client.Features.Collections;

namespace SetExplorer.Endpoints.Collections;

internal class RemoveCardFromCollectionEndpoint : Endpoint<RemoveCardFromCollectionRequest>
{
    private readonly CardCollectionService _cardCollectionService;

    public RemoveCardFromCollectionEndpoint(CardCollectionService cardCollectionService)
    {
        _cardCollectionService = cardCollectionService;
    }

    public override void Configure()
    {
        Delete("/collections/{collectionId:guid}/cards/{cardId:guid}");
        Description(x => x.Accepts<RemoveCardFromCollectionRequest>());
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
