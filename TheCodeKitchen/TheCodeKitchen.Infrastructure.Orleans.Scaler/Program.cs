

using TheCodeKitchen.Infrastructure.Orleans.Scaler.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ScaledObjectRefValidator>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ExternalScalerService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();