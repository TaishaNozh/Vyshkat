namespace VyshKat.Core.Domain;

///Обычный велосипед, энергию не потребляет
public sealed class Bicycle : Vehicle
{
    public Bicycle(string inventoryNumber, string name, int simplicity) : base(inventoryNumber, name, simplicity)
    {
    }
}