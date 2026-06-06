using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using CardExplorer.Client.Core;
using CardExplorer.Client.Core.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApiClients(new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddSharedConfiguration();
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

await builder.Build().RunAsync();
