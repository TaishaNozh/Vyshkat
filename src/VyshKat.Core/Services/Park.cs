using VyshKat.Core.Domain;
namespace VyshKat.Core.Services;

/// Хранилище объектов, стоящих на балансе парка
public sealed class Park
{
    private readonly List<IInventoryItem> _items = new();

    public IReadOnlyList<IInventoryItem> Items => _items;

    public bool Contains(string inventoryNumber) =>
        _items.Any(item => string.Equals(
            item.InventoryNumber, inventoryNumber, StringComparison.OrdinalIgnoreCase));

    public void Add(IInventoryItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (Contains(item.InventoryNumber))
            throw new InvalidOperationException(
                $"Объект с инвентарным номером {item.InventoryNumber} уже стоит на балансе");

        _items.Add(item);
    }
}