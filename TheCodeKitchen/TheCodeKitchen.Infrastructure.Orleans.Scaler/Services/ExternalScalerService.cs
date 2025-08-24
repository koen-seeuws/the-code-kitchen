using Externalscaler;
using FluentValidation;
using Grpc.Core;
using TheCodeKitchen.Infrastructure.Orleans.Scaler.Constants;
using TheCodeKitchen.Infrastructure.Orleans.Scaler.Extensions;
using TheCodeKitchen.Infrastructure.Orleans.Scaler.Validation;

namespace TheCodeKitchen.Infrastructure.Orleans.Scaler.Services;

public class ExternalScalerService(
    IClusterClient clusterClient,
    ScaledObjectRefValidator scaledObjectRefValidator
) : ExternalScaler.ExternalScalerBase
{
    private readonly IManagementGrain _managementGrain = clusterClient.GetGrain<IManagementGrain>(0);

    public override async Task<GetMetricsResponse> GetMetrics(GetMetricsRequest request, ServerCallContext context)
    {
        await ValidateRequestMetadata(request.ScaledObjectRef);

        var metricValue = await GetHighestSiloGrainCount();

        var response = new GetMetricsResponse();
        response.MetricValues.Add(new MetricValue
        {
            MetricName = OrleansScalerConstants.GrainCountMetricName,
            MetricValue_ = metricValue
        });

        return response;
    }

    public override async Task<GetMetricSpecResponse> GetMetricSpec(ScaledObjectRef request, ServerCallContext context)
    {
        await ValidateRequestMetadata(request);

        var metricThreshold = request.GetMaxGrainCountPerSilo();

        var response = new GetMetricSpecResponse();

        response.MetricSpecs.Add(new MetricSpec
        {
            MetricName = OrleansScalerConstants.GrainCountMetricName,
            TargetSize = metricThreshold
        });

        return response;
    }

    public override async Task<IsActiveResponse> IsActive(ScaledObjectRef request, ServerCallContext context)
    {
        await ValidateRequestMetadata(request);

        var metricValue = await GetHighestSiloGrainCount();
        var metricThreshold = request.GetMaxGrainCountPerSilo();

        return new IsActiveResponse
        {
            Result = metricValue >= metricThreshold
        };
    }

    public override async Task StreamIsActive(ScaledObjectRef request,
        IServerStreamWriter<IsActiveResponse> responseStream, ServerCallContext context)
    {
        await ValidateRequestMetadata(request);

        while (!context.CancellationToken.IsCancellationRequested)
        {
            var metricValue = await GetHighestSiloGrainCount();
            var metricThreshold = request.GetMaxGrainCountPerSilo();

            await responseStream.WriteAsync(new IsActiveResponse
            {
                Result = metricValue >= metricThreshold
            });

            await Task.Delay(TimeSpan.FromSeconds(30), context.CancellationToken);
        }
    }

    private async Task ValidateRequestMetadata(ScaledObjectRef request)
    {
        var context = new ValidationContext<ScaledObjectRef>(request);
        var result = await scaledObjectRefValidator.ValidateAsync(context);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    private async Task<int> GetHighestSiloGrainCount()
    {
        var grainStatistics = await _managementGrain.GetSimpleGrainStatistics();

        var grainCountsPerSilo = grainStatistics
            .GroupBy(statistic => statistic.SiloAddress)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(statistic => statistic.ActivationCount)
            )
            .Select(group => group.Value)
            .ToList();
        
        return grainCountsPerSilo.Count != 0 ? grainCountsPerSilo.Max() : 0;
    }
}