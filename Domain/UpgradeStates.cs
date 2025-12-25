namespace Lab3_Inventory.Domain;

public interface IUpgradeState
{
    string Name { get; }

    bool TryUpgrade(UpgradableItem item);
}

public sealed class BasicUpgradeState : IUpgradeState
{
    public string Name => "Обычный";

    public bool TryUpgrade(UpgradableItem item)
    {
        item.ApplyUpgrade(2);
        item.SetState(new EnhancedUpgradeState());
        return true;
    }
}

public sealed class EnhancedUpgradeState : IUpgradeState
{
    public string Name => "Улучшенный";

    public bool TryUpgrade(UpgradableItem item)
    {
        item.ApplyUpgrade(3);
        item.SetState(new BestUpgradeState());
        return true;
    }
}

public sealed class BestUpgradeState : IUpgradeState
{
    public string Name => "Лучший";

    public bool TryUpgrade(UpgradableItem item) => false;
}
