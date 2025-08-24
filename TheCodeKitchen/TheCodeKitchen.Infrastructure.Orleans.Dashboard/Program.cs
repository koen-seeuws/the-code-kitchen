using Azure.Data.Tables;
using Orleans.Configuration;
using TheCodeKitchen.Infrastructure.Extensions;
using TheCodeKitchen.Infrastructure.Orleans;

var builder = Host.CreateApplicationBuilder(args);

var siloConfiguration =
    builder.Configuration
        .BindAndValidateConfiguration<OrleansConfiguration, OrleansConfigurationValidator>(
            "TheCodeKitchenOrleans");

var azureStorageConnectionString =
    builder.Configuration.GetConnectionString("AzureStorage") ??
    throw new InvalidOperationException("ConnectionStrings__AzureStorage is not configured.");

var tableClient = new TableServiceClient(azureStorageConnectionString);

builder.UseOrleans(silo =>
{
    silo.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = siloConfiguration.ClusterId;
        options.ServiceId = siloConfiguration.ServiceId;
    });

    if (builder.Environment.IsDevelopment())
        silo.ConfigureEndpoints(11112, 30001);

    silo.UseAzureStorageClustering(options =>
    {
        options.TableServiceClient = tableClient;
        options.TableName = "TheCodeKitchenClustering";
    });

    silo.UseDashboard(options => options.HostSelf = true);
});

var host = builder.Build();

host.Run();