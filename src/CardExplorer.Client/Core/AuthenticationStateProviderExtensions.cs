using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace CardExplorer.Client.Core;

public static class AuthenticationStateProviderExtensions
{
    extension(AuthenticationStateProvider authenticationStateProvider)
    {
        public async Task<UserId> GetUserIdAsync()
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            return UserId.Parse(state.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
        }
        
        public async Task<bool> IsAuthenticatedAsync() => (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity?.IsAuthenticated == true;
    }
    
}