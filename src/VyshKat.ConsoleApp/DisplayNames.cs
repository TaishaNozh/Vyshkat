using VyshKat.Core.Domain;
namespace VyshKat.ConsoleApp;

/// Названия видов объектов для вывода в консоль
internal static class DisplayNames
{
    public static string KindOf(IInventoryItem item) => item switch
    {
        Bicycle => "Велосипед",
        ElectricScooter => "Электросамокат",
        ElectricBike => "Электровелосипед",
        Helmet => "Шлем",
        Station => "Станция",
        ChargingCabinet => "Шкаф подзарядки",
        _ => item.GetType().Name,
    };
}