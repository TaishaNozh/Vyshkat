using VyshKat.Core.Domain;
using VyshKat.Core.Services;
namespace VyshKat.Tests.Services;

public class ParkReportsTests
{
    private readonly Park _park = new();
    private readonly ParkReports _reports;

    public ParkReportsTests()
    {
        _reports = new ParkReports(_park);
    }

    [Fact]
    public void EmptyPark_ReportsAreZero()
    {
        Assert.Equal(0, _reports.TotalCount);
        Assert.Equal(0, _reports.TotalDailyEnergyKwh);
        Assert.Empty(_reports.GetBeginnerFriendlyVehicles());
        Assert.Empty(_reports.GetInventory());
    }

    [Fact]
    public void Counts_SeparateVehiclesAndEquipment()
    {
        _park.Add(new Bicycle("VK-001", "Велосипед", 8));
        _park.Add(new ElectricScooter("VK-002", "Самокат", 7, 0.5));
        _park.Add(new Helmet("EQ-001", "Шлем"));

        Assert.Equal(3, _reports.TotalCount);
        Assert.Equal(2, _reports.VehicleCount);
        Assert.Equal(1, _reports.EquipmentCount);
    }

    [Fact]
    public void TotalDailyEnergyKwh_SumsOnlyEnergyConsumers()
    {
        _park.Add(new ElectricScooter("VK-001", "Самокат", 7, 0.5));
        _park.Add(new Station("EQ-001", "Станция", 1.5));
        _park.Add(new Bicycle("VK-002", "Велосипед", 8));
        _park.Add(new Helmet("EQ-002", "Шлем"));

        Assert.Equal(2.0, _reports.TotalDailyEnergyKwh, precision: 6);
    }

    [Fact]
    public void GetBeginnerFriendlyVehicles_ReturnsOnlyVehiclesWithSimplicityFromThreshold()
    {
        var easy = new Bicycle("VK-001", "Простой велосипед", Vehicle.BeginnerSimplicityThreshold);
        var hard = new ElectricScooter("VK-002", "Сложный самокат", Vehicle.BeginnerSimplicityThreshold - 1, 0.5);
        _park.Add(easy);
        _park.Add(hard);
        _park.Add(new Helmet("EQ-001", "Шлем"));

        var result = _reports.GetBeginnerFriendlyVehicles();

        Assert.Same(easy, Assert.Single(result));
    }

    [Fact]
    public void GetInventory_ContainsVehiclesAndEquipment()
    {
        _park.Add(new Bicycle("VK-001", "Велосипед", 8));
        _park.Add(new ChargingCabinet("EQ-001", "Шкаф", 2.4));

        var numbers = _reports.GetInventory().Select(item => item.InventoryNumber);

        Assert.Equal(new[] { "VK-001", "EQ-001" }, numbers);
    }
}