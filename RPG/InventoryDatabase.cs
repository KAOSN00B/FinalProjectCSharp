using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG
{
    public static class ItemDatabase
    {
        private static List<Inventory> items = new()
        {
            //Weapons
            new Weapon("Firebrand Greatsword", 8, 5),
            new Weapon("Iron Sword", 5, 15),
            new Weapon("Bronze Dagger", 4, 24),
            new Weapon("Small Fire Sword", 4, 5),
            new Weapon("Oak Wand", 3, 7),
            new Weapon("Hornet Stinger Dagger", 2, 5),
            
            //Armours
            new Armour("Leather Vest", 2, 3, 13),
            new Armour("Padded Jacket", 1, 3, 15),
            new Armour("Cloth Robe", 1, 3, 8),
            new Armour("Rock ChestPlate", 2, 1, 2),
            new Armour("Balrog Hide", 10, 3, 10),
            new Armour("Light Balrog Hide", 6, 3, 6),

            //Consumeables
            new Consumable("Skill Elixir", 0, 0, 0, 10, 3),
            new Consumable("Defense Tonic", 0, 0, 3, 8, 0),
            new Consumable("Strength Tonic", 0, 3, 0, 7,0),
            new Consumable("Health Potion", 20, 0, 0, 10, 0),
        };

        public static Inventory GetItem(string name)
        {
            var item = items.FirstOrDefault(i =>
                i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return item?.Copy();
        }

        public static Weapon GetWeapon(string name)
        {
            return items
                .OfType<Weapon>()
                .FirstOrDefault(w => w.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public static Armour GetArmour(string name)
        {
            return items
                .OfType<Armour>()
                .FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}



