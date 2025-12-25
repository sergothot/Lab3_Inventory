namespace Lab3_Inventory.Domain;

public sealed class Potion : Item
{
    public Potion(string name, string description, int charges)
        : base(name, description, ItemCategory.Potion, new PotionUseStrategy())
    {
        Charges = charges;
    }

    public int Charges { get; private set; }

    public void UseCharge()
    {
        if (Charges > 0)
        {
            Charges--;
        }
    }
}

public sealed class QuestItem : Item
{
    public QuestItem(string name, string description)
        : base(name, description, ItemCategory.Quest, new QuestItemUseStrategy())
    {
    }
}
