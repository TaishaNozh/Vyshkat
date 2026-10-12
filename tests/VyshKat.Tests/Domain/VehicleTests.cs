using VyshKat.Core.Domain;
namespace VyshKat.Tests.Domain;

public class VehicleTests
{
    [Theory]
    [InlineData(1, false)]
    [InlineData(5, false)]
    [InlineData(6, true)]
    [InlineData(10, true)]
    public void IsBeginnerFriendly_DependsOnSimplicityThreshold(int simplicity, bool expected)
    {
        var bicycle = new Bicycle("VK-001", "Велосипед", simplicity);

        Assert.Equal(expected, bicycle.IsBeginnerFriendly);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyInventoryNumber_Throws(string inventoryNumber)
    {
        Assert.Throws<ArgumentException>(() => new Bicycle(inventoryNumber, "Велосипед", 5));
    }

    [Fact]
    public void Constructor_EmptyName_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Bicycle("VK-001", "", 5));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    public void Constructor_SimplicityOutOfRange_Throws(int simplicity)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Bicycle("VK-001", "Велосипед", simplicity));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ElectricScooter_NonPositiveEnergy_Throws(double energy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ElectricScooter("VK-001", "Самокат", 5, energy));
    }

    [Fact]
    public void ElectricBike_StoresEnergyConsumption()
    {
        var bike = new ElectricBike("VK-001", "Электровелосипед", 5, 0.8);

        Assert.Equal(0.8, bike.DailyEnergyKwh);
    }
}