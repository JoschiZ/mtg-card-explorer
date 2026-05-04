using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using SetExplorer.Components.Account.Pages;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Account;

public record PerformExternalLoginRequest(string Provider, string? ReturnUrl);

public class PerformExternalLoginEndpoint(SignInManager<ApplicationUser> signInManager)
    : Endpoint<PerformExternalLoginRequest>
{
    public override void Configure()
    {
        Post("account/perform-external-login");
        AllowAnonymous();
        AllowFormData();
    }

    public override async Task HandleAsync(PerformExternalLoginRequest req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(req.Provider))
        {
            ThrowError(x => x.Provider, "Provider canot be empty");
            return;
        }

        IEnumerable<KeyValuePair<string, StringValues>> query = [
            new("ReturnUrl", req.ReturnUrl),
            new("Action", ExternalLogin.LoginCallbackAction)];

        var redirectUrl = UriHelper.BuildRelative(
            HttpContext.Request.PathBase,
            "api/account/ExternalLogin",
            QueryString.Create(query));

        var properties = signInManager.ConfigureExternalAuthenticationProperties(req.Provider, redirectUrl);
        await HttpContext.ChallengeAsync(req.Provider, properties);
    }
}
