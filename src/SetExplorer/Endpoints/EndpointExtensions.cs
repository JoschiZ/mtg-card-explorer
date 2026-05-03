using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SetExplorer.Client.Core;
using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Data;

namespace SetExplorer.Endpoints;

public static class EndpointExtensions
{
    extension<TRequest, TResponse>(Endpoint<TRequest, TResponse> endpoint) where TRequest : notnull
    {
        public UserId GetUserId()
        {
            var idClaim = endpoint.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idClaim == null || !UserId.TryParse(idClaim, out var userId))
            {
                throw new UnauthorizedAccessException();
            }

            return userId;
        }
    }
}
