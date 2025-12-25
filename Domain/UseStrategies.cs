namespace Lab3_Inventory.Domain;

public sealed class NonUsableStrategy : IUseStrategy
{
    public UseResult Use(Item item) => new(false, $"{item.Name} нельзя использовать таким образом.");
}

public sealed class PotionUseStrategy : IUseStrategy
{
    public UseResult Use(Item item)
    {
        if (item is not Potion potion)
        {
            return new(false, "Это не зелье.");
        }

        if (potion.Charges <= 0)
        {
            return new(false, $"{potion.Name} пусто.");
        }

        potion.UseCharge();
        var message = $"Вы выпили {potion.Name}. Осталось: {potion.Charges}.";
        var remove = potion.Charges == 0;
        return new(true, message, remove);
    }
}

public sealed class QuestItemUseStrategy : IUseStrategy
{
    public UseResult Use(Item item)
    {
        if (item is not QuestItem questItem)
        {
            return new(false, "Это не квестовый предмет.");
        }

        return new(true, $"Вы исследуете {questItem.Name}: {questItem.Description}");
    }
}
