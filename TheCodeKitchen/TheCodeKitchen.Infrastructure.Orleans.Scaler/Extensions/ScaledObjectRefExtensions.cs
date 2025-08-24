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

        return 1000; // Default value if not specified
    }
}