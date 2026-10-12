namespace VyshKat.Core.Domain;


/// Объект, который стоит на балансе и имеет инвентарный номер
public interface IInventoryItem
{
    string InventoryNumber { get; }
    string Name { get; }
}