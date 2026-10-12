namespace VyshKat.Core.Domain;

/// Cстанция для парковки и зарядки транспорта
public sealed class Station : Equipment, IEnergyConsumer
{
    public double DailyEnergyKwh { get; }

    public Station(string inventoryNumber, string name, double dailyEnergyKwh) : base(inventoryNumber, name)
    {
        if (dailyEnergyKwh <= 0)
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyKwh), "Суточный расход энергии должен быть больше нуля");

        DailyEnergyKwh = dailyEnergyKwh;
    }
}
