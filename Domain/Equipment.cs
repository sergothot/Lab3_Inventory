namespace Lab3_Inventory.Domain;

public abstract class UpgradableItem : Item, IEquipable
{
    private IUpgradeState _state;

    protected UpgradableItem(string name, string description, ItemCategory category, IUpgradeState initialState)
        : base(name, description, category)
    {
        _state = initialState;
    }

    public abstract EquipSlot Slot { get; }

    public string UpgradeState => _state.Name;

    public bool TryUpgrade() => _state.TryUpgrade(this);

    internal void SetState(IUpgradeState newState) => _state = newState;

    internal abstract void ApplyUpgrade(int amount);
}

public sealed class Weapon : UpgradableItem
{
    public Weapon(string name, string description, int damage)
        : base(name, description, ItemCategory.Weapon, new BasicUpgradeState())
    {
        Damage = damage;
    }

    public int Damage { get; private set; }

    public override EquipSlot Slot => EquipSlot.WeaponHand;

    internal override void ApplyUpgrade(int amount)
    {
        Damage += amount;
    }
}

public sealed class Armor : UpgradableItem
{
    public Armor(string name, string description, int defense)
        : base(name, description, ItemCategory.Armor, new BasicUpgradeState())
    {
        Defense = defense;
    }

    public int Defense { get; private set; }

    public override EquipSlot Slot => EquipSlot.Body;

    internal override void ApplyUpgrade(int amount)
    {
        Defense += amount;
    }
}
