using Lab3_Inventory.Domain;
using Xunit;

namespace Lab3_Inventory.Tests;

public class InventoryTests
{
    [Fact]
    public void AddItem_RejectsDuplicateIds()
    {
        var inventory = new Inventory();
        var sword = new Weapon("Меч", "", 5);

        var first = inventory.AddItem(sword);
        var second = inventory.AddItem(sword);

        Assert.True(first);
        Assert.False(second);
        Assert.Single(inventory.Items);
    }

    [Fact]
    public void UsePotion_RemovesWhenEmpty()
    {
        var inventory = new Inventory();
        var potion = new Potion("Зелье лечения", "", 1);
        inventory.AddItem(potion);

        var result = inventory.UseItem(potion.Id);

        Assert.True(result.Success);
        Assert.DoesNotContain(inventory.Items, i => i.Id == potion.Id);
    }

    [Fact]
    public void EquipWeaponAndArmor_SetsSlots()
    {
        var inventory = new Inventory();
        var sword = new Weapon("Меч", "", 5);
        var armor = new Armor("Броня", "", 3);
        inventory.AddItem(sword);
        inventory.AddItem(armor);

        var weaponEquipped = inventory.EquipWeapon(sword.Id);
        var armorEquipped = inventory.EquipArmor(armor.Id);

        Assert.True(weaponEquipped);
        Assert.True(armorEquipped);
        Assert.Equal(sword, inventory.EquippedWeapon);
        Assert.Equal(armor, inventory.EquippedArmor);
    }

    [Fact]
    public void UpgradeItem_StopsAtMasterwork()
    {
        var inventory = new Inventory();
        var sword = new Weapon("Меч", "", 10);
        inventory.AddItem(sword);

        var first = inventory.UpgradeItem(sword.Id);
        var second = inventory.UpgradeItem(sword.Id);
        var third = inventory.UpgradeItem(sword.Id);

        Assert.True(first);
        Assert.True(second);
        Assert.False(third);
        Assert.Equal(15, sword.Damage);
        Assert.Equal("Лучший", sword.UpgradeState);
    }

    [Fact]
    public void BuilderAndFactory_CreateConfiguredItems()
    {
        var builder = new WeaponBuilder()
            .WithName("Топор")
            .WithDescription("Тяжелый топор")
            .WithDamage(12);
        var weapon = builder.Build();

        var factory = new StandardItemFactory();
        var questItem = factory.CreateQuestItem("Руна", "Древний камень");

        Assert.Equal("Топор", weapon.Name);
        Assert.Equal(12, weapon.Damage);

        var useResult = questItem.Use();
        Assert.True(useResult.Success);
        Assert.Contains("Руна", useResult.Message);
    }
}
