using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using POS.Client;
using Microsoft.AspNetCore.Components.Authorization;
using POS.Client.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthHeaderHandler>();

// 1. Registrar el proveedor personalizado
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthStateProvider>());

// 2. Habilitar la autorización en el cliente
builder.Services.AddAuthorizationCore();

// 3. (Opcional) Registrar el HttpClient para llamar a tu API
builder.Services.AddScoped(sp =>
{
    // Usamos el Handler que acabas de crear
    var handler = sp.GetRequiredService<AuthHeaderHandler>();
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7169/") // El puerto de tu API
    };
});
await builder.Build().RunAsync();
