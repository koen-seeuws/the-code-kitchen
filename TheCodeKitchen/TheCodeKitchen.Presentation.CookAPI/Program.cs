using TheCodeKitchen.Infrastructure.AzureServiceBus;
using TheCodeKitchen.Infrastructure.Security.Configuration;
using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Presentation.API.Cook.StreamSubscribers;
using TheCodeKitchen.Presentation.WebCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<NextMomentStreamSubscriber>();

// Infrastructure services
builder.Services.AddTheCodeKitchenOrleansClient(builder.Configuration, builder.Environment);
builder.Services.AddAzureServiceBusServices(builder.Configuration);
builder.Services.AddJwtSecurityServices(builder.Configuration);
builder.Services.AddPasswordHashingServices();
builder.Services.AddSignalRServices(builder.Configuration, builder.Environment);

// Presentation services
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();