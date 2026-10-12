using System.Globalization;
using VyshKat.Core.Domain;
using VyshKat.Core.Services;
namespace VyshKat.ConsoleApp;

/// Загрузка техники и вещей из текстового файла
/// Формат строки: вид;инвентарный номер;название;простота;расход кВт*ч
internal sealed class FileImporter
{
    private const char Separator = ';';
    private const int FieldCount = 5;

    private readonly ParkService _parkService;

    public FileImporter(ParkService parkService)
    {
        _parkService = parkService ?? throw new ArgumentNullException(nameof(parkService));
    }

    public void Import(string path)
    {
        string[] lines;

        try
        {
            lines = File.ReadAllLines(path);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.WriteLine($"Не удалось прочитать файл «{path}»: {ex.Message}");
            return;
        }

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();

            if (line.Length == 0 || line.StartsWith('#'))
                continue;

            try
            {
                ImportLine(line);
            }
            catch (Exception ex) when (ex is FormatException or ArgumentException or InvalidOperationException)
            {
                Console.WriteLine($"Строка {i + 1}: ошибка. {ex.Message}");
            }
        }
    }

    private void ImportLine(string line)
    {
        var parts = line.Split(Separator);

        if (parts.Length != FieldCount)
            throw new FormatException($"Ожидается {FieldCount} полей через «{Separator}»");

        var kind = parts[0].Trim().ToLowerInvariant();
        var number = parts[1].Trim();
        var name = parts[2].Trim();

        switch (kind)
        {
            case "bicycle":
                Accept(new Bicycle(number, name, ParseSimplicity(parts[3])));
                break;
            case "scooter":
                Accept(new ElectricScooter(number, name, ParseSimplicity(parts[3]), ParseEnergy(parts[4])));
                break;
            case "ebike":
                Accept(new ElectricBike(number, name, ParseSimplicity(parts[3]), ParseEnergy(parts[4])));
                break;
            case "helmet":
                Register(new Helmet(number, name));
                break;
            case "station":
                Register(new Station(number, name, ParseEnergy(parts[4])));
                break;
            case "cabinet":
                Register(new ChargingCabinet(number, name, ParseEnergy(parts[4])));
                break;
            default:
                throw new FormatException($"Неизвестный вид «{kind}»");
        }
    }

    private void Accept(Vehicle vehicle)
    {
        var result = _parkService.AcceptVehicle(vehicle);
        var status = result.IsApproved ? "принят" : "ОТКЛОНЁН";
        Console.WriteLine($"{vehicle.InventoryNumber} «{vehicle.Name}»: {status}. {result.Reason}");
    }

    private void Register(Equipment equipment)
    {
        _parkService.RegisterEquipment(equipment);
        Console.WriteLine($"{equipment.InventoryNumber} «{equipment.Name}»: на балансе");
    }

    private static int ParseSimplicity(string text) =>
        int.TryParse(text.Trim(), out var value)
            ? value
            : throw new FormatException($"Простота «{text}» не является целым числом");

    private static double ParseEnergy(string text) =>
        double.TryParse(text.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
            ? value
            : throw new FormatException($"Расход энергии «{text}» не является числом");
}