using VyshKat.Core.Domain;
namespace VyshKat.Core.Services;

/// Отчеты по парку
public sealed class ParkReports
{
    private readonly Park _park;

    public ParkReports(Park park)
    {
        _park = park ?? throw new ArgumentNullException(nameof(park));
    }

    public int TotalCount => _park.Items.Count;

    public int VehicleCount => _park.Items.OfType<Vehicle>().Count();

    public int EquipmentCount => _park.Items.OfType<Equipment>().Count();

    /// Суммарный суточный расход энергии всех потребителей, кВт*ч
    public double TotalDailyEnergyKwh =>
        _park.Items.OfType<IEnergyConsumer>().Sum(consumer => consumer.DailyEnergyKwh);

    public IReadOnlyList<Vehicle> GetBeginnerFriendlyVehicles() =>
        _park.Items.OfType<Vehicle>().Where(vehicle => vehicle.IsBeginnerFriendly).ToList();

    public IReadOnlyList<IInventoryItem> GetInventory() => _park.Items;
}