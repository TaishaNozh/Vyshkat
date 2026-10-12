namespace VyshKat.Core.Domain;

/// Электросамокат
public sealed class ElectricScooter : Vehicle, IEnergyConsumer
{
    public double DailyEnergyKwh { get; }

    public ElectricScooter(string inventoryNumber, string name, int simplicity, double dailyEnergyKwh) : base(inventoryNumber, name, simplicity)
    {
        if (dailyEnergyKwh <= 0)
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyKwh), "Суточный расход энергии должен быть больше нуля");

        DailyEnergyKwh = dailyEnergyKwh;
    }
}