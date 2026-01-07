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
        private bool VisitedShop = false;
        public List<Weapon> WeaponsForSale { get; private set; }
        public List<Armour> ArmourForSale { get; private set; }

        public List<Consumable> ConsumableForSale { get; private set; }

        public Shop()
        {
            WeaponsForSale = new List<Weapon>()
        {
            new Weapon("Iron Sword", 5, 15),
            new Weapon("Bronze Dagger", 4, 24),
            new Weapon("Oak Wand", 3, 7)
        };

            ArmourForSale = new List<Armour>()
        {
            new Armour("Leather Vest", 2, 3, 13),
            new Armour("Padded Jacket", 1, 3, 15),
            new Armour("Cloth Robe", 1, 3, 8)
        };

            ConsumableForSale = new List<Consumable>()
            {
                new Consumable("Skill Elixir", 0, 0, 0, 10, 3),
                new Consumable("Defense Tonic", 0, 0, 3, 8, 0),
                new Consumable("Strength Tonic", 0, 3, 0, 7,0),
                new Consumable("Health Potion", 20, 0, 0, 10, 0),
            };
        }

        public void ShopMenu(Player player)
        {

            if (VisitedShop == false)
            {
                Console.WriteLine($"Well if it isn't my old friend {player.Name}. What can i do for you?");
            }
            else
            {
                Console.WriteLine("What would you like to do?");
            }
            Console.WriteLine("1. Weapons");
            Console.WriteLine("2. Armour");
            Console.WriteLine("3. Items");
            Console.WriteLine("4. Sell Item");
            Console.WriteLine("0. Exit Shop/Back");
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
                        VisitedShop = true;
                        break;

                    case 2:
                        ArmourDisplay(player);
                        VisitedShop = true;
                        break;

                    case 3:
                        ItemDisplay(player);
                        VisitedShop = true;
                        break;

                    case 4:
                        SellItem(player);
                        VisitedShop = true;
                        break;

                    case 0:
                        Console.WriteLine("Thank you for visiting friend. See you again!");
                        VisitedShop = false;
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid shop option.\n");
                        continue;
                }
                VisitedShop = false;
                break;
            }
        }

        public void SellItem(Player player)
        {
            if (player.PlayerInventory.Count == 0)
            {
                Console.WriteLine("You have no items to sell.");
                return;
            }

            Console.WriteLine("Your Inventory:");
            int index = 1;

            foreach (var item in player.PlayerInventory)
            {
                Console.WriteLine($"{index}) {item.Name} | Sell Price: {item.Price / 2} gold");
                index++;
            }

            Console.WriteLine("\nEnter the number of the item you want to sell (or 0 to cancel):");

            string choice = Console.ReadLine();

            if (!int.TryParse(choice, out int selectedIndex) ||
                selectedIndex < 0 || selectedIndex > player.PlayerInventory.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            if (selectedIndex == 0)
            {
                Console.WriteLine("No worries What would you like to do?");
                return;
            }



            var itemToSell = player.PlayerInventory[selectedIndex - 1];

            int sellPrice = itemToSell.Price / 2;

            player.Gold += sellPrice;
            player.PlayerInventory.Remove(itemToSell);

            Console.WriteLine($"You sold {itemToSell.Name} for {sellPrice} gold.");
            Console.WriteLine($"You now have {player.Gold} gold.");
            return;
        }


        public void WeaponDisplay(Player player)
        {
            Console.WriteLine($"Gold: {player.Gold}");
            while (true)
            {
                Console.WriteLine("\nWeapons for Sale: (0 to go back)");

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
                    player.Gold -= selected.Price;
                    Console.WriteLine($"You purchased {selected.Name}!");
                    Console.WriteLine($"You have {player.Gold} gold left.");
                    player.PlayerInventory.Add(selected);
                }

            }
            return;
        }
        public void ArmourDisplay(Player player)
        {
            Console.WriteLine($"Gold: {player.Gold}");
            while (true)
            {
                Console.WriteLine("\nArmour for Sale: (0 to go back)");

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
                    player.Gold -= selected.Price;
                    Console.WriteLine($"You purchased {selected.Name}!");
                    Console.WriteLine($"You have {player.Gold} gold left.");
                    player.PlayerInventory.Add(selected);
                }
            }
            return;
        }

        public void ItemDisplay(Player player)
        {
            Console.WriteLine($"Gold: {player.Gold}");
            while (true)
            {
                Console.WriteLine("\nItems for Sale: (0 to go back)");

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
                    player.Gold -= selected.Price;
                    Console.WriteLine($"You purchased {selected.Name}!");
                    Console.WriteLine($"You have {player.Gold} gold left.");
                    player.PlayerInventory.Add(selected);
                }
            }
            return;
        }

    }
}


