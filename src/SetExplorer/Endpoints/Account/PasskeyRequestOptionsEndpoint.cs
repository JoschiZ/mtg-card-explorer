using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Account;

public record PasskeyRequestOptionsRequest(string? Username);

public class PasskeyRequestOptionsEndpoint(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    : Endpoint<PasskeyRequestOptionsRequest>
{
    public override void Configure()
    {
        Post("/Account/PasskeyRequestOptions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PasskeyRequestOptionsRequest req, CancellationToken ct)
    {
        var user = string.IsNullOrEmpty(req.Username) ? null : await userManager.FindByNameAsync(req.Username);
        var optionsJson = await signInManager.MakePasskeyRequestOptionsAsync(user);
        
        await Send.StringAsync(optionsJson, contentType: "application/json", cancellation: ct);
    }
}
