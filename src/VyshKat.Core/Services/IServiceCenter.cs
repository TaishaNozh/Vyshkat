using VyshKat.Core.Domain;
namespace VyshKat.Core.Services;

/// Сервис техосмотра, который решает, можно ли принять транспорт в парк
public interface IServiceCenter
{
    InspectionResult Inspect(Vehicle vehicle);
}
