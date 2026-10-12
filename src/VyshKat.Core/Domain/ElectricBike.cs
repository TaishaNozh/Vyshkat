namespace VyshKat.Core.Domain;

/// Электровелосипед
public sealed class ElectricBike : Vehicle, IEnergyConsumer
{
    public double DailyEnergyKwh { get; }

    public ElectricBike(string inventoryNumber, string name, int simplicity, double dailyEnergyKwh) : base(inventoryNumber, name, simplicity)
    {
        if (dailyEnergyKwh <= 0)
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyKwh), "Суточный расход энергии должен быть больше нуля");

        DailyEnergyKwh = dailyEnergyKwh;
    }
}

