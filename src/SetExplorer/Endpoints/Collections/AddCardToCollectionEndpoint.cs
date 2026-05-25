using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Collections;
using CollectionId = SetExplorer.Client.Features.Collections.CollectionId;


namespace SetExplorer.Endpoints.Collections;

internal class AddCardToCollectionEndpoint : FastEndpoints.Endpoint<AddCardToCollectionRequest>
{
    private readonly CardCollectionService _cardCollectionService;

    public AddCardToCollectionEndpoint(CardCollectionService cardCollectionService)
    {
        _cardCollectionService = cardCollectionService;
    }

    public override void Configure()
    {
        Post("/collections/{collectionId:guid}/cards/{cardId:guid}");
    }

    public override async Task HandleAsync(AddCardToCollectionRequest req, CancellationToken ct)
    {
        var userId = this.GetUserId();


        var result = await _cardCollectionService.RetrieveAndAddCardToCollection(userId, req, ct);

        await result.Match(
            _ => Send.OkAsync(cancellation: ct),
            _ => Send.NotFoundAsync(ct)
        );
    }
}
