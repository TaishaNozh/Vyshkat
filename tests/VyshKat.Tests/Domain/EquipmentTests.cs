using VyshKat.Core.Domain;
namespace VyshKat.Tests.Domain;

public class EquipmentTests
{
    [Fact]
    public void Helmet_StoresInventoryNumberAndName()
    {
        var helmet = new Helmet("EQ-001", "Шлем, размер M");

        Assert.Equal("EQ-001", helmet.InventoryNumber);
        Assert.Equal("Шлем, размер M", helmet.Name);
    }

    [Fact]
    public void Helmet_IsNotEnergyConsumer()
    {
        IInventoryItem helmet = new Helmet("EQ-001", "Шлем");

        Assert.False(helmet is IEnergyConsumer);
    }

    [Fact]
    public void Constructor_EmptyInventoryNumber_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Helmet("", "Шлем"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2.5)]
    public void Station_NonPositiveEnergy_Throws(double energy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Station("EQ-002", "Станция", energy));
    }

    [Fact]
    public void ChargingCabinet_StoresEnergyConsumption()
    {
        var cabinet = new ChargingCabinet("EQ-003", "Шкаф", 2.4);

        Assert.Equal(2.4, cabinet.DailyEnergyKwh);
    }
}