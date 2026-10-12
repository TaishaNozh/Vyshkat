using VyshKat.Core.Domain;
using VyshKat.Core.Services;
namespace VyshKat.ConsoleApp;

/// Главное меню консольного приложения
internal sealed class ConsoleMenu
{
    private readonly ParkService _parkService;
    private readonly ParkReports _reports;
    private readonly FileImporter _importer;

    public ConsoleMenu(ParkService parkService, ParkReports reports, FileImporter importer)
    {
        _parkService = parkService ?? throw new ArgumentNullException(nameof(parkService));
        _reports = reports ?? throw new ArgumentNullException(nameof(reports));
        _importer = importer ?? throw new ArgumentNullException(nameof(importer));
    }

    public void Run()
    {
        Console.WriteLine("ВышКат — учёт парка! Доедем до дедлайна!");
        Console.WriteLine();

        while (true)
        {
            PrintMenu();
            var choice = ConsoleInput.ReadInt("Выберите пункт: ", 0, 6);
            Console.WriteLine();

            if (choice == 0)
            {
                Console.WriteLine("До свидания!");
                return;
            }

            try
            {
                Execute(choice);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("1. Принять транспорт (с техосмотром)");
        Console.WriteLine("2. Поставить вещь на баланс");
        Console.WriteLine("3. Отчёт: число единиц и расход энергии");
        Console.WriteLine("4. Транспорт для новичков");
        Console.WriteLine("5. Инвентарный список");
        Console.WriteLine("6. Загрузить технику и вещи из файла");
        Console.WriteLine("0. Выход");
    }

    private void Execute(int choice)
    {
        switch (choice)
        {
            case 1:
                AcceptVehicle();
                break;
            case 2:
                RegisterEquipment();
                break;
            case 3:
                PrintSummary();
                break;
            case 4:
                PrintBeginnerFriendly();
                break;
            case 5:
                PrintInventory();
                break;
            case 6:
                var path = ConsoleInput.ReadText("Путь к файлу (например, demo-data.csv): ");
                _importer.Import(path.Trim('"', '\''));
                break;
        }
    }

    private void AcceptVehicle()
    {
        Console.WriteLine("Вид транспорта: 1 — велосипед, 2 — электросамокат, 3 — электровелосипед");
        var kind = ConsoleInput.ReadInt("Вид: ", 1, 3);
        var number = ConsoleInput.ReadText("Инвентарный номер: ");
        var name = ConsoleInput.ReadText("Название: ");
        var simplicity = ConsoleInput.ReadInt(
            $"Простота для новичка ({Vehicle.MinSimplicity}–{Vehicle.MaxSimplicity}): ",
            Vehicle.MinSimplicity,
            Vehicle.MaxSimplicity);

        Vehicle vehicle = kind switch
        {
            1 => new Bicycle(number, name, simplicity),
            2 => new ElectricScooter(number, name, simplicity, ReadEnergy()),
            _ => new ElectricBike(number, name, simplicity, ReadEnergy()),
        };

        var result = _parkService.AcceptVehicle(vehicle);

        Console.WriteLine(result.IsApproved
            ? $"Принят в парк: {vehicle.InventoryNumber}. {result.Reason}"
            : $"Отклонён техосмотром. {result.Reason}");
    }

    private void RegisterEquipment()
    {
        Console.WriteLine("Вид вещи: 1 — шлем, 2 — станция, 3 — шкаф подзарядки");
        var kind = ConsoleInput.ReadInt("Вид: ", 1, 3);
        var number = ConsoleInput.ReadText("Инвентарный номер: ");
        var name = ConsoleInput.ReadText("Название: ");

        Equipment equipment = kind switch
        {
            1 => new Helmet(number, name),
            2 => new Station(number, name, ReadEnergy()),
            _ => new ChargingCabinet(number, name, ReadEnergy()),
        };

        _parkService.RegisterEquipment(equipment);
        Console.WriteLine($"Поставлено на баланс: {equipment.InventoryNumber}");
    }

    private static double ReadEnergy() =>
        ConsoleInput.ReadPositiveDouble("Суточный расход энергии, кВт*ч: ");

    private void PrintSummary()
    {
        Console.WriteLine($"Всего на балансе: {_reports.TotalCount}");
        Console.WriteLine($"  транспорт: {_reports.VehicleCount}");
        Console.WriteLine($"  вещи: {_reports.EquipmentCount}");
        Console.WriteLine($"Суммарный суточный расход энергии: {_reports.TotalDailyEnergyKwh:0.##} кВт*ч");
    }

    private void PrintBeginnerFriendly()
    {
        var vehicles = _reports.GetBeginnerFriendlyVehicles();

        if (vehicles.Count == 0)
        {
            Console.WriteLine("Транспорта для новичков пока нет");
            return;
        }

        Console.WriteLine($"Можно выдавать новичкам (простота ≥ {Vehicle.BeginnerSimplicityThreshold}):");
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine($"  {vehicle.InventoryNumber,-8} {DisplayNames.KindOf(vehicle),-17} " + $"{vehicle.Name} (простота {vehicle.Simplicity})");
        }
    }

    private void PrintInventory()
    {
        var items = _reports.GetInventory();

        if (items.Count == 0)
        {
            Console.WriteLine("На балансе пока ничего нет");
            return;
        }

        Console.WriteLine("Инвентарный список:");
        foreach (var item in items)
            Console.WriteLine($"  {item.InventoryNumber,-8} {DisplayNames.KindOf(item),-17} {item.Name}");
    }
}