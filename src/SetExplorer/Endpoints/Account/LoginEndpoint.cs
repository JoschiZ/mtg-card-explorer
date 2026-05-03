using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using SetExplorer.Data;

namespace SetExplorer.Endpoints.Account;

public record LoginRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public bool RememberMe { get; init; }
}

public record LoginResponse
{
    public required string Status { get; init; }
    public string? Message { get; init; }
}

public class LoginEndpoint(SignInManager<ApplicationUser> signInManager, ILogger<LoginEndpoint> logger) 
    : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post("/account/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        // Perform password sign-in
        var result = await signInManager.PasswordSignInAsync(req.Email, req.Password, req.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            logger.LogInformation("User {Email} logged in via API.", req.Email);
            await SendOkAsync(new LoginResponse { Status = "Success" }, ct);
            return;
        }

        if (result.RequiresTwoFactor)
        {
            await SendAsync(new LoginResponse { Status = "RequiresTwoFactor" }, 202, ct);
            return;
        }

        if (result.IsLockedOut)
        {
            logger.LogWarning("User account {Email} locked out.", req.Email);
            await SendAsync(new LoginResponse { Status = "LockedOut" }, 403, ct);
            return;
        }

        await SendAsync(new LoginResponse { Status = "Failure", Message = "Invalid login attempt." }, 401, ct);
    }
}
