using Externalscaler;

namespace TheCodeKitchen.Infrastructure.Orleans.Scaler.Extensions;

public static class ScaledObjectRefExtensions
{
    public static long GetMaxGrainCountPerSilo(this ScaledObjectRef scaledObjectRef)
    {
        if (scaledObjectRef.ScalerMetadata != null &&
            scaledObjectRef.ScalerMetadata.TryGetValue(Constants.OrleansScalerConstants.MaxGrainCountPerSiloMetadataKey, out var maxGrainCountPerSiloString) &&
            long.TryParse(maxGrainCountPerSiloString, out var maxGrainCountPerSilo))
        {
            return maxGrainCountPerSilo;
        }

        throw new InvalidOperationException($"ScaledObjectRef is missing required metadata key {Constants.OrleansScalerConstants.MaxGrainCountPerSiloMetadataKey}");
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