using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace RPG
{
    public class Shop
    {
        public List<Weapon> WeaponsForSale { get; private set; }
        public List<Armour> ArmourForSale { get; private set; }

        public List<Consumable> ConsumableForSale { get; private set; }

        public Shop()
        {
            WeaponsForSale = new List<Weapon>()
        {
            new Weapon("Iron Sword", 5),
            new Weapon("Bronze Dagger", 4),
            new Weapon("Oak Wand", 3)
        };

            ArmourForSale = new List<Armour>()
        {
            new Armour("Leather Vest", 2, 3),
            new Armour("Padded Jacket", 1, 3),
            new Armour("Cloth Robe", 1, 3)
        };

            ConsumableForSale = new List<Consumable>()
            {
                new Consumable("Health Potion", 20, 0, 0, 0),
                new Consumable("Skill Elixir", 0, 0, 0, 1),
                new Consumable("Defense Tonic", 0, 0, 3, 0),
                new Consumable("Strength Tonic", 0, 3, 0, 0)
            };
        }

        public void ShopMenu(Player player)
        {
            Console.WriteLine("Welcome to the shop!");
            Console.WriteLine("What would you like to buy?");
            Console.WriteLine("1. Weapons");
            Console.WriteLine("2. Armour");
            Console.WriteLine("3. Items");
            Console.WriteLine("0. Exit Shop");
            Console.WriteLine("(press at any time to 9 Display Inventory)");
            int choice;

            while (true)
            {
                Console.Write("Enter choice: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a NUMBER.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        WeaponDisplay(player);
                        break;

                    case 2:
                        ArmourDisplay(player);
                        break;

                    case 3:
                        ItemDisplay(player);
                        break;

                    case 0:
                        Console.WriteLine("Thank you for visiting the shop!");
                        return;  

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid shop option.\n");
                        continue;
                }

                break; 
            }
        }

        public void WeaponDisplay(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nWeapons for Sale:");

                for (int i = 0; i < WeaponsForSale.Count; i++)
                {
                    Weapon weapon = WeaponsForSale[i];
                    weapon.DisplayInfo();
                }

                string input = Console.ReadLine();
                string cleanedInput = input.Replace(" ", "").ToUpper();

                Weapon selected = WeaponsForSale.FirstOrDefault(w => w.Name.Replace(" ", "").ToUpper() == cleanedInput);


                foreach (Weapon weapon in WeaponsForSale)
                {
                    if (weapon.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        selected = weapon;
                        break;
                    }
                }

                if (input == "0")
                {
                    ShopMenu(player);
                    break;
                }

                if (input == "9")
                {
                    player.DisplayInventory();
                    continue;
                }

                if (selected == null)
                {
                    Console.WriteLine("Weapon not found. Please type an exact name.\n");
                }
                else
                {
                    Console.WriteLine($"You purchased {selected.Name}!");
                    player.Inventory.Add(selected);
                }

            }
        }
        public void ArmourDisplay(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nArmour for Sale:");

                for (int i = 0; i < ArmourForSale.Count; i++)
                {
                    Armour armour = ArmourForSale[i];
                    armour.DisplayInfo();
                }

                string input = Console.ReadLine();
                string cleanedInput = input.Replace(" ", "").ToUpper();

                Armour selected = ArmourForSale.FirstOrDefault(w => w.Name.Replace(" ", "").ToUpper() == cleanedInput);


                foreach (Armour armour in ArmourForSale)
                {
                    if (armour.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        selected = armour;
                        break;
                    }
                }

                if (input == "0")
                {
                    ShopMenu(player);
                    break;
                }

                if (input == "9")
                {
                    player.DisplayInventory();
                    continue;
                }

                if (selected == null)
                {
                    Console.WriteLine("Weapon not found. Please type an exact name.\n");
                }
                else
                {
                    Console.WriteLine($"You purchased {selected.Name}!");
                    player.Inventory.Add(selected);
                }
            }
        }

        public void ItemDisplay(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nItems for Sale:");

                for (int i = 0; i < ConsumableForSale.Count; i++)
                {
                    Consumable consumable = ConsumableForSale[i];
                    consumable.DisplayInfo();
                }

                string input = Console.ReadLine();
                string cleanedInput = input.Replace(" ", "").ToUpper();

                Consumable selected = ConsumableForSale.FirstOrDefault(w => w.Name.Replace(" ", "").ToUpper() == cleanedInput);

                foreach (Consumable consumables in ConsumableForSale)
                {
                    if (consumables.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        selected = consumables;
                        break;
                    }
                }

                if (input == "0")
                {
                    ShopMenu(player);
                    break;
                }

                if (input == "9")
                {
                    player.DisplayInventory();
                    continue;
                }

                if (selected == null)
                {
                    Console.WriteLine("Weapon not found. Please type an exact name.\n");
                }
                else
                {
                    Console.WriteLine($"You purchased {selected.Name}!");
                    player.Inventory.Add(selected);
                }
            }
        }

    }
}

    
