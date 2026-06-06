using FastEndpoints;
using FastEndpoints.OpenApi;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using MudBlazor.Services;
using Scalar.AspNetCore;
using CardExplorer.Client.Core;
using CardExplorer.Client.Core.Scryfall;
using CardExplorer.Client.Features.Collections;
using CardExplorer.Client.Features.Explorations;
using CardExplorer.Components;
using CardExplorer.Components.Account;
using CardExplorer.Components.Collections;
using CardExplorer.Components.Explorations;
using CardExplorer.Data;
using CardExplorer.Endpoints.Collections;
using CardExplorer.Endpoints.Explorations;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
// Add MudBlazor services
builder.Services.AddMudServices();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddHttpLogging();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddSharedConfiguration();
builder.Services.AddFastEndpoints();
builder.Services.OpenApiDocument(o =>
{
    o.Title = "CardExplorer API";
    o.Version = "v1";
    o.AddAuth("Identity.Application", new OpenApiSecurityScheme()
    {
        
        Type = SecuritySchemeType.ApiKey,
        Name = ".AspNetCore.Identity.Application",
        In = ParameterLocation.Cookie,
        Description = "Enter your .AspNetCore.Identity.Application cookie",
    });

    o.AutoTagPathSegmentIndex = 1;
    o.UseOneOfForPolymorphism = true;
});
builder.Services.AddOpenApi();
builder.Services.AddAntiforgery();

builder.Services.AddScoped<ICollectionsClient, ServerCollectionsClient>();
builder.Services.AddScoped<CardCollectionService>();
builder.Services.AddScoped<IExplorationsClient, ServerExplorationsClient>();
builder.Services.AddScoped<ExplorationService>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var app = builder.Build();
app.UseHttpLogging();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseFastEndpoints(config =>
{
    config.Endpoints.RoutePrefix = "api";
});

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CardExplorer.Client._Imports).Assembly);

app.Run();
