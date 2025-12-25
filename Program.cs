using Lab3_Inventory.Domain;

var factory = new StandardItemFactory();
var inventory = new Inventory();

var sword = factory.CreateWeapon("Бронзовый меч", "Надежный для боямеч", 5);
var armor = factory.CreateArmor("Кожаный жилет", "Небольшая защита", 3);
var potion = factory.CreatePotion("Лечебное зелье", "Восстанавливает немного здоровья", 2);

inventory.AddItem(sword);
inventory.AddItem(armor);
inventory.AddItem(potion);

inventory.EquipWeapon(sword.Id);
inventory.EquipArmor(armor.Id);

var useResult = inventory.UseItem(potion.Id);
Console.WriteLine(useResult.Message);
Console.WriteLine(inventory.Describe());
