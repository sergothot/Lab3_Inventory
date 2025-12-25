using System.Collections.Generic;
using System.Linq;

namespace Lab3_Inventory.Domain;

public sealed class Inventory
{
    private readonly List<Item> _items = new();

    public Weapon? EquippedWeapon { get; private set; }

    public Armor? EquippedArmor { get; private set; }

    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

    public bool AddItem(Item item)
    {
        if (_items.Any(i => i.Id == item.Id))
        {
            return false;
        }

        _items.Add(item);
        return true;
    }

    public bool RemoveItem(Guid itemId)
    {
        var found = _items.FirstOrDefault(i => i.Id == itemId);
        if (found is null)
        {
            return false;
        }

        var removed = _items.Remove(found);

        if (EquippedWeapon?.Id == itemId)
        {
            EquippedWeapon = null;
        }

        if (EquippedArmor?.Id == itemId)
        {
            EquippedArmor = null;
        }

        return removed;
    }

    public UseResult UseItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item is null)
        {
            return new UseResult(false, "Предмет не найден.");
        }

        var result = item.Use();
        if (result.RemoveFromInventory)
        {
            _items.Remove(item);
        }

        return result;
    }

    public bool EquipWeapon(Guid itemId)
    {
        var weapon = _items.OfType<Weapon>().FirstOrDefault(w => w.Id == itemId);
        if (weapon is null)
        {
            return false;
        }

        EquippedWeapon = weapon;
        return true;
    }

    public bool EquipArmor(Guid itemId)
    {
        var armor = _items.OfType<Armor>().FirstOrDefault(a => a.Id == itemId);
        if (armor is null)
        {
            return false;
        }

        EquippedArmor = armor;
        return true;
    }

    public bool UpgradeItem(Guid itemId)
    {
        var upgradable = _items.OfType<UpgradableItem>().FirstOrDefault(i => i.Id == itemId);
        return upgradable?.TryUpgrade() ?? false;
    }

    public string Describe()
    {
        var summary = new List<string>
        {
            $"Предметы: {_items.Count}",
            $"Оружие: {EquippedWeapon?.Name ?? "Нет"}",
            $"Броня: {EquippedArmor?.Name ?? "Нет"}"
        };

        foreach (var item in _items)
        {
            summary.Add($"- {item.Name} ({item.Category})");
        }

        return string.Join('\n', summary);
    }
}
