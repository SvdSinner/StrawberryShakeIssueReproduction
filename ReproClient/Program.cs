using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReproClient;
using StrawberryShake;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//StrawberryShake
builder.Services.AddAccountClient(ExecutionStrategy.CacheAndNetwork)
    .ConfigureHttpClient(client =>
        client.BaseAddress = new Uri("https://localhost:7039/graphql"))
    .ConfigureWebSocketClient(client => client.Uri = new Uri("wss://localhost:7039/graphql"));

await builder.Build().RunAsync();
