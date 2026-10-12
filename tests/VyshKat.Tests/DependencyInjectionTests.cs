using Microsoft.Extensions.DependencyInjection;
using VyshKat.Core.Domain;
using VyshKat.Core.Services;
using VyshKat.Tests.Fakes;
namespace VyshKat.Tests;

public class DependencyInjectionTests
{
    private static ServiceProvider BuildProvider(IServiceCenter serviceCenter)
    {
        var services = new ServiceCollection();
        services.AddSingleton<Park>();
        services.AddSingleton(serviceCenter);
        services.AddSingleton<ParkService>();
        services.AddSingleton<ParkReports>();
        return services.BuildServiceProvider();
    }

    [Fact]
    public void Container_WithFakeServiceCenter_ParkServiceUsesFake()
    {
        var fake = FakeServiceCenter.AlwaysReject();
        using var provider = BuildProvider(fake);

        var parkService = provider.GetRequiredService<ParkService>();
        var reports = provider.GetRequiredService<ParkReports>();

        // Настоящий техосмотр принял бы велосипед, значит, отказ пришёл от фейка
        var result = parkService.AcceptVehicle(new Bicycle("VK-001", "Велосипед", 8));

        Assert.False(result.IsApproved);
        Assert.Single(fake.InspectedVehicles);
        Assert.Equal(0, reports.TotalCount);
    }

    [Fact]
    public void Container_SharesSingleParkBetweenServices()
    {
        using var provider = BuildProvider(FakeServiceCenter.AlwaysApprove());

        var parkService = provider.GetRequiredService<ParkService>();
        var reports = provider.GetRequiredService<ParkReports>();

        parkService.AcceptVehicle(new Bicycle("VK-001", "Велосипед", 8));

        Assert.Equal(1, reports.TotalCount);
    }
}