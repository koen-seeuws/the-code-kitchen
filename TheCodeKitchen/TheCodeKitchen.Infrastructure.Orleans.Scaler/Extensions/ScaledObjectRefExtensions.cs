using Externalscaler;

namespace TheCodeKitchen.Infrastructure.Orleans.Scaler.Extensions;

public static class ScaledObjectRefExtensions
{
    public static long GetAverageGrainCountPerSilo(this ScaledObjectRef scaledObjectRef)
    {
        if (scaledObjectRef.ScalerMetadata != null &&
            scaledObjectRef.ScalerMetadata.TryGetValue(Constants.OrleansScalerConstants.AverageGrainCountPerSiloMetadataKey, out var averageGrainCountPerSiloString) &&
            long.TryParse(averageGrainCountPerSiloString, out var averageGrainCountPerSilo))
        {
            return averageGrainCountPerSilo;
        }

        throw new InvalidOperationException($"ScaledObjectRef is missing required metadata key {Constants.OrleansScalerConstants.AverageGrainCountPerSiloMetadataKey}");
    }
    
    public static string GetSiloNameFilter(this ScaledObjectRef scaledObjectRef)
    {
        if (scaledObjectRef.ScalerMetadata != null &&
            scaledObjectRef.ScalerMetadata.TryGetValue(Constants.OrleansScalerConstants.SiloNameFilterMetadataKey, out var siloNameFilter) &&
            !string.IsNullOrWhiteSpace(siloNameFilter))
        {
            return siloNameFilter.Trim();
        }

        throw new InvalidOperationException($"ScaledObjectRef is missing required metadata key {Constants.OrleansScalerConstants.SiloNameFilterMetadataKey}");
    }
}