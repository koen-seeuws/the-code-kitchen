using TheCodeKitchen.Application.Constants;

namespace TheCodeKitchen.Presentation.API.Cook.Controllers.Equipment;

[Tags("Equipment - Mixer")]
public class MixerController(IClusterClient clusterClient) : EquipmentController(clusterClient, EquipmentType.Mixer);