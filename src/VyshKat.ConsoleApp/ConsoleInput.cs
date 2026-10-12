using System.Globalization;
namespace VyshKat.ConsoleApp;

/// Чтение значений из консоли с повтором при некорректном вводе
internal static class ConsoleInput
{
    public static string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = ReadLineOrExit().Trim();

            if (text.Length > 0)
                return text;

            Console.WriteLine("Значение не может быть пустым");
        }
    }

    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = ReadLineOrExit();

            if (int.TryParse(text, out var value) && value >= min && value <= max)
                return value;

            Console.WriteLine($"Введите целое число от {min} до {max}");
        }
    }

    public static double ReadPositiveDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = ReadLineOrExit().Replace(',', '.');

            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
                && value > 0)
                return value;

            Console.WriteLine("Введите положительное число, например 0.5");
        }
    }

    private static string ReadLineOrExit()
    {
        var line = Console.ReadLine();

        // null означает, что ввод закончился
        if (line is null)
        {
            Console.WriteLine();
            Environment.Exit(0);
        }

        return line;
    }
}