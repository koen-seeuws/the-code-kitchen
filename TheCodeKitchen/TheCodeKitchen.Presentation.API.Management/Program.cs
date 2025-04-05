using TheCodeKitchen.Application.Business;
using TheCodeKitchen.Infrastructure.AzureServiceBus;
using TheCodeKitchen.Infrastructure.Common;
using TheCodeKitchen.Infrastructure.DataAccess;
using TheCodeKitchen.Infrastructure.Security.Configuration;
using TheCodeKitchen.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Application services
builder.Services.AddApplicationServices();

// Infrastructure services
builder.Services.AddAzureServiceBusServices(builder.Configuration);
builder.Services.AddCommonInfrastructure();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddPasswordHashingServices();
builder.Services.AddJwtSecurityServices(builder.Configuration);
builder.Services.AddSignalRServices();

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