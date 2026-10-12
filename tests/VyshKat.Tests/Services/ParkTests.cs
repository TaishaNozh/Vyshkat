using VyshKat.Core.Domain;
using VyshKat.Core.Services;
namespace VyshKat.Tests.Services;

public class ParkTests
{
    [Fact]
    public void Add_NewItem_AppearsInItems()
    {
        var park = new Park();
        var helmet = new Helmet("EQ-001", "Шлем");

        park.Add(helmet);

        Assert.Same(helmet, Assert.Single(park.Items));
        Assert.True(park.Contains("EQ-001"));
    }

    [Fact]
    public void Add_DuplicateInventoryNumber_Throws()
    {
        var park = new Park();
        park.Add(new Bicycle("VK-001", "Велосипед", 8));

        Assert.Throws<InvalidOperationException>(
            () => park.Add(new Helmet("vk-001", "Шлем с тем же номером")));
    }
}