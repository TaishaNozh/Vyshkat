namespace VyshKat.Core.Domain;

/// Вещь на балансе, конкретные виды вещей наследуются от этого класса
public abstract class Equipment : IInventoryItem
{
    public string InventoryNumber { get; }
    public string Name { get; }

    protected Equipment(string inventoryNumber, string name)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер обязателен", nameof(inventoryNumber));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название обязательно", nameof(name));

        InventoryNumber = inventoryNumber;
        Name = name;
    }
}