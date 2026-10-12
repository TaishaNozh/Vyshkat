using VyshKat.Core.Domain;
namespace VyshKat.Core.Services;

/// Приём объектов на баланс, транспорт принимается только после техосмотра, а вещи принимаются без него.
public sealed class ParkService
{
    private readonly IServiceCenter _serviceCenter;
    private readonly Park _park;

    public ParkService(IServiceCenter serviceCenter, Park park)
    {
        _serviceCenter = serviceCenter ?? throw new ArgumentNullException(nameof(serviceCenter));
        _park = park ?? throw new ArgumentNullException(nameof(park));
    }

    /// Проводит техосмотр и, если он пройден, ставит транспорт на баланс
    public InspectionResult AcceptVehicle(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        var result = _serviceCenter.Inspect(vehicle);

        if (result.IsApproved)
            _park.Add(vehicle);

        return result;
    }

    /// Ставит вещь на баланс
    public void RegisterEquipment(Equipment equipment)
    {
        ArgumentNullException.ThrowIfNull(equipment);
        _park.Add(equipment);
    }
}