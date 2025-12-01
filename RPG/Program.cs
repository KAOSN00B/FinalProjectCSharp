using RPG;

internal class Program
{
    private static void Main(string[] args)
    {
        // Create the player using your CharacterCreator()
        Player player = Player.CharacterCreator();

        Console.WriteLine("\n--- Player Created ---");
        player.DisplayStats();

        // Create some weapons — matching your constructor (name, hpBonus, attackBonus, defenseBonus)
        Weapon ironSword = new Weapon("Iron Sword", 0, 3, 0);
        Weapon fireBlade = new Weapon("Fire Blade", 0, 7, 0);
        Weapon megaAxe = new Weapon("Mega Axe", 0, 10, 0);
        Armour ironArmour = new Armour("Iron Armour", 6, 0, 3);
        Armour fireArmour = new Armour("Fire Armour", 1, 0, 5);
        Armour megaArmour = new Armour("Mega Armour", 3, 0, 5);

        // Add weapons to inventory
        player.Inventory.Add(ironSword);
        player.Inventory.Add(fireBlade);
        player.Inventory.Add(megaAxe);
        player.Inventory.Add(megaArmour);
        player.Inventory.Add(ironArmour);
        player.Inventory.Add(fireArmour);

        Console.WriteLine("\n--- Inventory Loaded ---");
        foreach (var w in player.Inventory)
        {
            Console.WriteLine($"{w.Name} (HP+{w.HPBonus} ATK+{w.AttackBonus} DEF+{w.DefenseBonus})");
        }

        // Equip Iron Sword
        Console.WriteLine("\n--- Equipping Iron Sword ---");
        player.EquipWeapon(ironSword);
        player.DisplayStats();

        Console.WriteLine(("\n--- Equipping Mega Armour ---"));
        player.EquipArmour(megaArmour);
        player.DisplayStats();

        player.RemoveWeapon(ironSword);
        player.RemoveArmour(megaArmour);
        player.DisplayStats();

        // Equip Fire Blade
        Console.WriteLine("\n--- Switching to Fire Blade ---");
        player.EquipWeapon(fireBlade);
        player.DisplayStats();

        // Equip Mega Axe
        Console.WriteLine("\n--- Switching to Mega Axe ---");
        player.EquipWeapon(megaAxe);
        player.DisplayStats();

        Console.WriteLine("\nProgram finished. Press any key to exit.");
        Console.ReadKey();
    }
}
