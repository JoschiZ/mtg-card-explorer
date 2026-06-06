using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using CardExplorer.Components.Account.Pages.Manage;
using CardExplorer.Data;

namespace CardExplorer.Endpoints.Account;

public record LinkExternalLoginRequest(string Provider);

public class LinkExternalLoginEndpoint(SignInManager<ApplicationUser> signInManager)
    : Endpoint<LinkExternalLoginRequest>
{
    public override void Configure()
    {
        Post("Account/Manage/LinkExternalLogin");
        AllowFormData();
    }

    public override async Task HandleAsync(LinkExternalLoginRequest req, CancellationToken ct)
    {
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        var redirectUrl = UriHelper.BuildRelative(
            HttpContext.Request.PathBase,
            "/Account/Manage/ExternalLogins",
            QueryString.Create("Action", ExternalLogins.LinkLoginCallbackAction));

        var properties = signInManager.ConfigureExternalAuthenticationProperties(req.Provider, redirectUrl, signInManager.UserManager.GetUserId(HttpContext.User));
        await HttpContext.ChallengeAsync(req.Provider, properties);
    }
}
