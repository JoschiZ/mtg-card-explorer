using Microsoft.AspNetCore.Components.Authorization;
using CardExplorer.Client.Core;
using CardExplorer.Client.Core.Http;
using CardExplorer.Client.Features.Collections;
using CardExplorer.Endpoints.Collections;

namespace CardExplorer.Components.Collections;

internal class ServerCollectionsClient : ICollectionsClient
{
    private readonly CardCollectionService _cardCollectionService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public ServerCollectionsClient(CardCollectionService cardCollectionService, AuthenticationStateProvider authenticationStateProvider)
    {
        _cardCollectionService = cardCollectionService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<CardCollectionDto[]> GetCollectionsAsync(GetCollectionsRequest request, CancellationToken ct = default)
    {
        var userId = await _authenticationStateProvider.GetUserIdAsync();
        var result = await _cardCollectionService.SearchCollectionsAsync(userId, request, ct);
        return result.Match(x => x);
    }

    public async  Task<CardCollectionDto?> CreateCollectionAsync(CreateCollectionRequest request, CancellationToken ct = default)
    {
        var userId = await _authenticationStateProvider.GetUserIdAsync();
        var result = await _cardCollectionService.CreateNewCollection(userId, request, ct);
        return result.Match(x => x);
    }

    public async  Task AddCardToCollectionAsync(AddCardToCollectionRequest request, CancellationToken ct = default)
    {
        var userId = await _authenticationStateProvider.GetUserIdAsync();
        await _cardCollectionService.RetrieveAndAddCardToCollection(userId, request, ct);
    }

    public async Task RemoveCardFromCollectionAsync(RemoveCardFromCollectionRequest request, CancellationToken ct = default)
    {
        var userId = await _authenticationStateProvider.GetUserIdAsync();
        await _cardCollectionService.RemoveCardAsync(userId, request, ct);
    }
}