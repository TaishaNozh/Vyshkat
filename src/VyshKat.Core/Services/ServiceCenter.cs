using VyshKat.Core.Domain;
namespace VyshKat.Core.Services;

/// Отклоняет электротранспорт с высоким суточным расходом энергии, т.к. это признак неисправного аккумулятора
public sealed class ServiceCenter : IServiceCenter
{
    public const double MaxDailyEnergyKwh = 2.0;

    public InspectionResult Inspect(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);

        if (vehicle is IEnergyConsumer consumer && consumer.DailyEnergyKwh > MaxDailyEnergyKwh)
        {
            return InspectionResult.Rejected(
                $"Суточный расход {consumer.DailyEnergyKwh} кВт*ч превышает допустимые " +
                $"{MaxDailyEnergyKwh} кВт*ч: возможна неисправность аккумулятора");
        }

        return InspectionResult.Approved();
    }
}