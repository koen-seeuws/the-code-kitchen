using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Infrastructure.Security.Configuration;
using TheCodeKitchen.Presentation;
using TheCodeKitchen.Presentation.ManagementAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure services
builder.Services.AddTheCodeKitchenOrleansClient(builder.Configuration);
builder.Services.AddPasswordHashingServices();
builder.Services.AddJwtSecurityServices(builder.Configuration);
builder.Services.AddAzureSignalRServices(builder.Configuration);

// Presentation services
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.MapHub<GameManagementHub>("/gamemanagementhub");
app.MapOpenApi();

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();