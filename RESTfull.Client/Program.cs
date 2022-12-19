using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using RESTfull.Client;
using RESTfull.Client.Interfaces;
using RESTfull.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorageAsSingleton();

var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7000") });
builder.Services.AddSingleton<TokenService, TokenService>();
builder.Services.AddRefitClient<IUsersClient>(settings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7000"));
builder.Services.AddRefitClient<IChatClient>(settings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7000"));

await builder.Build().RunAsync();