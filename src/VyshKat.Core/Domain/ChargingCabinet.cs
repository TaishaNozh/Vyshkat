namespace VyshKat.Core.Domain;

/// Шкаф подзарядки для аккумуляторов 
public sealed class ChargingCabinet : Equipment, IEnergyConsumer
{
    public double DailyEnergyKwh { get; }

    public ChargingCabinet(string inventoryNumber, string name, double dailyEnergyKwh) : base(inventoryNumber, name)
    {
        if (dailyEnergyKwh <= 0)
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyKwh), "Суточный расход энергии должен быть больше нуля.");

        DailyEnergyKwh = dailyEnergyKwh;
    }
}