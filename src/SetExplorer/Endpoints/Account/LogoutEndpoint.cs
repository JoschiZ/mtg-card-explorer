using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Account;

public record LogoutRequest([property:QueryParam]string? ReturnUrl);

public class LogoutEndpoint(SignInManager<ApplicationUser> signInManager)
    : Endpoint<LogoutRequest>
{
    public override void Configure()
    {
        Post("/Account/Logout");
    }

    public override async Task HandleAsync(LogoutRequest req, CancellationToken ct)
    {
        await signInManager.SignOutAsync();
     
        var returnUrl = string.IsNullOrWhiteSpace(req.ReturnUrl) ? null : req.ReturnUrl;
        await Send.RedirectAsync(req.ReturnUrl ?? "/");
    }
}
