using TheCodeKitchen.Infrastructure.Security.Configuration;
using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Presentation.API.Cook.Hubs;
using TheCodeKitchen.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure services
builder.Services.AddTheCodeKitchenOrleansClient(builder.Configuration, builder.Environment);
builder.Services.AddJwtSecurityServices(builder.Configuration);
builder.Services.AddPasswordHashingServices();
builder.Services.AddSignalRServices(builder.Configuration, builder.Environment);

// Presentation services
builder.Services.AddSingleton(typeof(StreamSubscriber<,>));

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.MapHub<KitchenHub>("/kitchenhub");
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();