using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Infrastructure.Orleans.Client;
using TheCodeKitchen.Infrastructure.Security.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure services
builder.Logging.AddConsole();
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

app.MapOpenApi();

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();