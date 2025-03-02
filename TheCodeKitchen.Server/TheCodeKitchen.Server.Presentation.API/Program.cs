using TheCodeKitchen.Server.Application.Business.Configuration;
using TheCodeKitchen.Server.Infrastructure.AzureServiceBus;
using TheCodeKitchen.Server.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

//Application services
builder.Services.AddApplicationServices(builder.Configuration);

//Infrastructure services
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddAzureServiceBusServices(builder.Configuration);

//Presentation services
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();