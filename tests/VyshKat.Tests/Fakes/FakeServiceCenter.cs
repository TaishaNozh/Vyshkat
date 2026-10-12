using VyshKat.Core.Domain;
using VyshKat.Core.Services;
namespace VyshKat.Tests.Fakes;

/// Подменный техосмотр для тестов, который всегда возвращает заранее заданное решение и запоминает, какой транспорт ему передали
internal sealed class FakeServiceCenter : IServiceCenter
{
    private readonly InspectionResult _result;
    public FakeServiceCenter(InspectionResult result)
    {
        _result = result;
    }

    public List<Vehicle> InspectedVehicles { get; } = new();
    public InspectionResult Inspect(Vehicle vehicle)
    {
        InspectedVehicles.Add(vehicle);
        return _result;
    }

    public static FakeServiceCenter AlwaysApprove() =>
        new(InspectionResult.Approved());
    public static FakeServiceCenter AlwaysReject() =>
        new(InspectionResult.Rejected("Отклонено фейковым техосмотром"));
}