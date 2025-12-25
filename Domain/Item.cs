using System;

namespace Lab3_Inventory.Domain;

public enum ItemCategory
{
    Weapon,
    Armor,
    Potion,
    Quest
}

public sealed record UseResult(bool Success, string Message, bool RemoveFromInventory = false);

public interface IUseStrategy
{
    UseResult Use(Item item);
}

public interface IEquipable
{
    EquipSlot Slot { get; }
}

public enum EquipSlot
{
    WeaponHand,
    Body
}

public abstract class Item
{
    protected Item(string name, string description, ItemCategory category, IUseStrategy? useStrategy = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Category = category;
        UseStrategy = useStrategy ?? new NonUsableStrategy();
    }

    public Guid Id { get; }

    public string Name { get; }

    public string Description { get; }

    public ItemCategory Category { get; }

    public IUseStrategy UseStrategy { get; }

    public UseResult Use() => UseStrategy.Use(this);
}
