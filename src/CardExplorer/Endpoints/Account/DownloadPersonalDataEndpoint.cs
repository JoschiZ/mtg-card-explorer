using System.Text.Json;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using CardExplorer.Data;

namespace CardExplorer.Endpoints.Account;

public class DownloadPersonalDataEndpoint(UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory)
    : EndpointWithoutRequest
{
    private readonly ILogger _logger = loggerFactory.CreateLogger("DownloadPersonalData");

    public override void Configure()
    {
        Post("/Account/Manage/DownloadPersonalData");
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
        _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);

        // Only include personal data for download
        var personalData = new Dictionary<string, string>();
        var personalDataProps = typeof(ApplicationUser).GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
        foreach (var p in personalDataProps)
        {
            personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
        }

        var logins = await userManager.GetLoginsAsync(user);
        foreach (var l in logins)
        {
            personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
        }

        personalData.Add("Authenticator Key", (await userManager.GetAuthenticatorKeyAsync(user))!);
        var fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

        await Send.BytesAsync(fileBytes, "PersonalData.json", "application/json", cancellation: ct);
    }
}
