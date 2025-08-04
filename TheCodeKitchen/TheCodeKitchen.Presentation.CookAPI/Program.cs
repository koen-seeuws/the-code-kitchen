using Microsoft.OpenApi.Models;
using TheCodeKitchen.Infrastructure.AzureSignalR;
using TheCodeKitchen.Infrastructure.Security.Configuration;
using TheCodeKitchen.Presentation;
using TheCodeKitchen.Presentation.API.Cook.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure services
builder.Services.AddTheCodeKitchenOrleansClient(builder.Configuration, builder.Environment);
builder.Services.AddJwtSecurityServices(builder.Configuration);
builder.Services.AddPasswordHashingServices();
builder.Services.AddAzureSignalRServices(builder.Configuration);

// Presentation services
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(swagger =>
{
    // Add Bearer token support
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.MapHub<KitchenHub>("/kitchenhub");
app.MapHub<CookHub>("/cookhub");
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();