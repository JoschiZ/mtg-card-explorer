using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace SetExplorer.Client.Core;

public static class AuthenticationStateProviderExtensions
{
    extension(AuthenticationStateProvider authenticationStateProvider)
    {
        public async Task<UserId> GetUserIdAsync()
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            return UserId.Parse(state.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
        }
    }
    
}