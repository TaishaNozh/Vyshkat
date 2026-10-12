namespace VyshKat.Core.Domain;

/// Прокатный транспорт, конкретные виды транспорта наследуются от этого класса
public abstract class Vehicle : IInventoryItem
{
    public const int MinSimplicity = 1;
    public const int MaxSimplicity = 10;

    /// Минимальная простота, при которой транспорт можно выдавать новичкам
    public const int BeginnerSimplicityThreshold = 6;
    public string InventoryNumber { get; }
    public string Name { get; }
    public int Simplicity { get; }

    protected Vehicle(string inventoryNumber, string name, int simplicity)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер обязателен", nameof(inventoryNumber));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название обязательно", nameof(name));

        if (simplicity < MinSimplicity || simplicity > MaxSimplicity)
            throw new ArgumentOutOfRangeException(nameof(simplicity),
                $"Простота должна быть от {MinSimplicity} до {MaxSimplicity}");

        InventoryNumber = inventoryNumber;
        Name = name;
        Simplicity = simplicity;
    }

    /// Можно ли выдавать транспорт новичкам
    public bool IsBeginnerFriendly => Simplicity >= BeginnerSimplicityThreshold;
}