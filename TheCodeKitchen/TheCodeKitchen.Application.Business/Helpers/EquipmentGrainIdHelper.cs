using TheCodeKitchen.Core.Enums;

namespace TheCodeKitchen.Application.Business.Helpers;

public static class EquipmentGrainIdHelper
{
    public static string CreateId(EquipmentType equipmentType, int number)
    {
        return $"{equipmentType}+{number}";
    }
}