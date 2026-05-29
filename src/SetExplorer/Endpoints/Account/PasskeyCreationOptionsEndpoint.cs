using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Account;

public class PasskeyCreationOptionsEndpoint(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("Account/PasskeyCreationOptions");
        Description(x => x.ClearDefaultAccepts());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        if (user is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var userId = await userManager.GetUserIdAsync(user);
        var userName = await userManager.GetUserNameAsync(user) ?? "User";
        var optionsJson = await signInManager.MakePasskeyCreationOptionsAsync(new()
        {
            Id = userId,
            Name = userName,
            DisplayName = userName
        });
        
        await Send.StringAsync(optionsJson, contentType: "application/json", cancellation: ct);
    }
}
