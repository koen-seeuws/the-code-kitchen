using MudBlazor.Services;
using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Infrastructure.Orleans.Client;
using TheCodeKitchen.Presentation.ManagementUI.Components;
using TheCodeKitchen.Presentation.ManagementUI.Hubs;
using TheCodeKitchen.Presentation.ManagementUI.Mapping;
using TheCodeKitchen.Presentation.ManagementUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(GameMapping));

// Infrastructure services
builder.Services.AddTheCodeKitchenOrleansClient(builder.Configuration);
builder.Services.AddAzureSignalRServices(builder.Configuration);

// Presentation services
builder.Services.AddMudServices();
builder.Services.AddScoped<ClientTimeService>();
builder.Services.AddScoped<ScrollService>();

builder.Services
    .AddRazorComponents()
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

app.MapHub<GameManagementHub>("/GameManagementHub");
app.MapHub<GameHub>("/GameHub");
app.MapHub<KitchenHub>("/KitchenHub");
app.MapHub<KitchenOrderHub>("/KitchenOrderHub");

app.Run();