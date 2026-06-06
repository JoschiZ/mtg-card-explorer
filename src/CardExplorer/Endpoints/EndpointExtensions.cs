using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using CardExplorer.Client.Core;


using CardExplorer.Data;

namespace CardExplorer.Endpoints;

public static class EndpointExtensions
{
    public static UserId GetUserId<TRequest, TResponse>(this Endpoint<TRequest, TResponse> endpoint) where TRequest : notnull
    {
        var idClaim = endpoint.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (idClaim == null || !UserId.TryParse(idClaim, out var userId))
        {
            throw new UnauthorizedAccessException();
        }

        return userId;
    }

    public static UserId GetUserId<TRequest>(this Endpoint<TRequest> endpoint) where TRequest : notnull
    {
        var idClaim = endpoint.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (idClaim == null || !UserId.TryParse(idClaim, out var userId))
        {
            throw new UnauthorizedAccessException();
        }

        return userId;
    }
}
