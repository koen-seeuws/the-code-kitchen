using MudBlazor.Services;
using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Presentation;
using TheCodeKitchen.Presentation.ManagementUI.Components;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure services
builder.Services.AddTheCodeKitchenOrleansClient(builder.Configuration, builder.Environment);
builder.Services.AddSignalRServices(builder.Configuration, builder.Environment);

// Presentation services
builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();