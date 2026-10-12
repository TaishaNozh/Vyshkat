namespace VyshKat.Core.Domain;

/// Шлем, энергию не потребляет
public sealed class Helmet : Equipment
{
    public Helmet(string inventoryNumber, string name) : base(inventoryNumber, name)
    {
    }
}