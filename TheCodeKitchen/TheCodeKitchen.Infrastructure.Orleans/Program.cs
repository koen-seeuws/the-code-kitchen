using TheCodeKitchen.Application.Business;
using TheCodeKitchen.Infrastructure.Security.Configuration;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();

builder.Services.AddPasswordHashingServices();

builder.UseOrleans(silo =>
{
    if (builder.Environment.IsDevelopment())
    {
        silo.UseLocalhostClustering();
        silo.AddMemoryGrainStorageAsDefault();
        silo.ConfigureLogging(logging => { logging.AddConsole(); });
    }
    
    silo.UseDashboard();
});

var host = builder.Build();
host.Run();