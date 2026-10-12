namespace VyshKat.Core.Domain;

/// Устройство, которое потребляет электроэнергию
public interface IEnergyConsumer
{
    /// Суточный расход энергии, кВт*ч
    double DailyEnergyKwh { get; }
}