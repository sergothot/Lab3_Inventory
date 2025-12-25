namespace Lab3_Inventory.Domain;

public interface IItemFactory
{
    Weapon CreateWeapon(string name, string description, int damage);
    Armor CreateArmor(string name, string description, int defense);
    Potion CreatePotion(string name, string description, int charges);
    QuestItem CreateQuestItem(string name, string description);
}

public sealed class StandardItemFactory : IItemFactory
{
    public Weapon CreateWeapon(string name, string description, int damage) => new(name, description, damage);

    public Armor CreateArmor(string name, string description, int defense) => new(name, description, defense);

    public Potion CreatePotion(string name, string description, int charges) => new(name, description, charges);

    public QuestItem CreateQuestItem(string name, string description) => new(name, description);
}

public sealed class WeaponBuilder
{
    private string _name = "Безымянное оружие";
    private string _description = "";
    private int _damage = 1;

    public WeaponBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public WeaponBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public WeaponBuilder WithDamage(int damage)
    {
        _damage = damage;
        return this;
    }

    public Weapon Build() => new(_name, _description, _damage);
}

public sealed class ArmorBuilder
{
    private string _name = "Безымянная броня";
    private string _description = "";
    private int _defense = 1;

    public ArmorBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ArmorBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ArmorBuilder WithDefense(int defense)
    {
        _defense = defense;
        return this;
    }

    public Armor Build() => new(_name, _description, _defense);
}
