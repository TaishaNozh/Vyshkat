using VyshKat.Core.Domain;
using VyshKat.Core.Services;
namespace VyshKat.Tests.Services;

public class ServiceCenterTests
{
    private readonly ServiceCenter _serviceCenter = new();

    [Fact]
    public void Inspect_Bicycle_Approves()
    {
        var result = _serviceCenter.Inspect(new Bicycle("VK-001", "Велосипед", 8));

        Assert.True(result.IsApproved);
    }

    [Fact]
    public void Inspect_EnergyAtLimit_Approves()
    {
        var scooter = new ElectricScooter("VK-001", "Самокат", 7, ServiceCenter.MaxDailyEnergyKwh);

        var result = _serviceCenter.Inspect(scooter);

        Assert.True(result.IsApproved);
    }

    [Fact]
    public void Inspect_EnergyAboveLimit_RejectsWithReason()
    {
        var scooter = new ElectricScooter("VK-001", "Самокат", 7, ServiceCenter.MaxDailyEnergyKwh + 0.5);

        var result = _serviceCenter.Inspect(scooter);

        Assert.False(result.IsApproved);
        Assert.False(string.IsNullOrWhiteSpace(result.Reason));
    }
}