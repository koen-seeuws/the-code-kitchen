namespace TheCodeKitchen.Application.Business.Helpers;

public static class EquipmentGrainIdHelper
{
    public static string CreateId(string equipmentType, int number)
    {
        return $"{equipmentType}+{number}";
    }
}