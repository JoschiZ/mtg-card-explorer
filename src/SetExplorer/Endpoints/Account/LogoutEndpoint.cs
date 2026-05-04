using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Account;

public record LogoutRequest(string? ReturnUrl);

public class LogoutEndpoint(SignInManager<ApplicationUser> signInManager)
    : Endpoint<LogoutRequest>
{
    public override void Configure()
    {
        Post("/Account/Logout");
        AllowAnonymous();
        AllowFormData();
    }

    public override async Task HandleAsync(LogoutRequest req, CancellationToken ct)
    {
        await signInManager.SignOutAsync();
        await Send.RedirectAsync(req.ReturnUrl ?? "");
    }
}
