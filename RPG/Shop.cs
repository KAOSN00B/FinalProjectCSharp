using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Shop
    {
        public List<Weapon> WeaponsForSale { get; private set; }
        public List<Armour> ArmourForSale { get; private set; }

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
        }

        public void ShopMenu(Player player)
        {
            Console.WriteLine("Welcome to the shop!");
            Console.WriteLine("What would you like to buy?");
            Console.WriteLine("1. Weapons");
            Console.WriteLine("2. Armour");
            Console.WriteLine("3. Exit Shop");
            Console.WriteLine("(press at any time to 9 Display Inventory)");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    WeaponDisplay(player);
                    break;
                case 2:
                    ArmourDisplay(player);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            Console.WriteLine("Weapons for sale:\n");

            
        }

        public void WeaponDisplay(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nWeapons for Sale:");

                for (int i = 0; i < WeaponsForSale.Count; i++)
                {
                    Weapon weapon = WeaponsForSale[i];
                    Console.WriteLine($"{i + 1}. {weapon.Name} - Attack Bonus: {weapon.AttackBonus}");
                }
                Console.Write("Choose a weapon to buy: ");
                Console.WriteLine("Enter 0 to exit to main shop menu.");

                string input = Console.ReadLine();

                // Validate number
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n");
                    continue;
                }

                // Exit option
                if (choice == 0)
                {
                    Console.WriteLine("Leaving shop...");
                    ShopMenu(player);
                }

                if (choice == 9)
                {
                    player.DisplayInventory();
                    continue;
                }

                // Out of range?
                if (choice < 1 || choice > WeaponsForSale.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.\n");
                    continue;
                }

                

                // Buy weapon
                Weapon selected = WeaponsForSale[choice - 1];
                Console.WriteLine($"You purchased {selected.Name}!");

                player.Inventory.Add(selected);
            }
        }
        public void ArmourDisplay(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nWeapons for Sale:");

                for (int i = 0; i < ArmourForSale.Count; i++)
                {
                    Armour armour = ArmourForSale[i];
                    Console.WriteLine($"{i + 1}. {armour.Name} - Attack Bonus: {armour.AttackBonus}");
                }
                Console.Write("Choose a weapon to buy: ");
                Console.WriteLine("Enter 0 to exit to main shop menu.");

                string input = Console.ReadLine();

                // Validate number
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n");
                    continue;
                }

                // Exit option
                if (choice == 0)
                {
                    Console.WriteLine("Leaving shop...");
                    ShopMenu(player);
                }

                if (choice == 9)
                {
                    player.DisplayInventory();
                    continue;
                }

                // Out of range?
                if (choice < 1 || choice > ArmourForSale.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.\n");
                    continue;
                }

                

                // Buy weapon
                Armour selected = ArmourForSale[choice - 1];
                Console.WriteLine($"You purchased {selected.Name}!");

                player.Inventory.Add(selected);
            }
        }



    }
}

    
