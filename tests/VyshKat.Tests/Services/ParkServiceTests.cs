using VyshKat.Core.Domain;
using VyshKat.Core.Services;
using VyshKat.Tests.Fakes;
namespace VyshKat.Tests.Services;

public class ParkServiceTests
{
    private readonly Park _park = new();

    [Fact]
    public void AcceptVehicle_WhenInspectionRejects_VehicleIsNotAdded()
    {
        var service = new ParkService(FakeServiceCenter.AlwaysReject(), _park);

        var result = service.AcceptVehicle(new ElectricScooter("VK-001", "Самокат", 7, 0.4));

        Assert.False(result.IsApproved);
        Assert.Empty(_park.Items);
    }

    [Fact]
    public void AcceptVehicle_WhenInspectionApproves_VehicleIsAdded()
    {
        var service = new ParkService(FakeServiceCenter.AlwaysApprove(), _park);
        var scooter = new ElectricScooter("VK-001", "Самокат", 7, 0.4);

        var result = service.AcceptVehicle(scooter);

        Assert.True(result.IsApproved);
        Assert.Same(scooter, Assert.Single(_park.Items));
    }

    [Fact]
    public void AcceptVehicle_PassesVehicleToServiceCenter()
    {
        var serviceCenter = FakeServiceCenter.AlwaysApprove();
        var service = new ParkService(serviceCenter, _park);
        var bicycle = new Bicycle("VK-001", "Велосипед", 8);

        service.AcceptVehicle(bicycle);

        Assert.Same(bicycle, Assert.Single(serviceCenter.InspectedVehicles));
    }

    [Fact]
    public void AcceptVehicle_DuplicateInventoryNumber_Throws()
    {
        var service = new ParkService(FakeServiceCenter.AlwaysApprove(), _park);
        service.AcceptVehicle(new Bicycle("VK-001", "Велосипед", 8));

        Assert.Throws<InvalidOperationException>(
            () => service.AcceptVehicle(new Bicycle("VK-001", "Другой велосипед", 8)));
    }

    [Fact]
    public void RegisterEquipment_AddsWithoutInspection()
    {
        var serviceCenter = FakeServiceCenter.AlwaysReject();
        var service = new ParkService(serviceCenter, _park);

        service.RegisterEquipment(new Helmet("EQ-001", "Шлем"));

        Assert.Single(_park.Items);
        Assert.Empty(serviceCenter.InspectedVehicles);
    }
}