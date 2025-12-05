using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class GameManager
    {
        public Player player;
        public List<Weapon> AllWeapons = new List<Weapon>
        {
            new Weapon("Iron Sword", 5),
            new Weapon("Bronze Dagger", 3),
            new Weapon("Oak Staff", 2),
        };

        public List<Armour> AllArmour = new List<Armour>
          {
            new Armour("Leather Vest", 3, 3),
            new Armour("Chainmail", 5, 3),
        };

        public GameManager() { }

        public void StartGame()
        {
            Console.WriteLine("Welcome to the RPG Game!");
            player = Player.CharacterCreator();
            WorldSelection();
        }
        public void WorldSelection()
        {

            Console.WriteLine("Where would you like to go?");
            Console.WriteLine("1. Forest Area");
            Console.WriteLine("2. Town Area");
            Console.WriteLine("3. Mountain Area");
            Console.WriteLine("4. Boss' Castle");
            int choice;

            // ask at least once
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid choice. Please select a valid area (1–4):");
            }

            switch (choice)
                {
                    case 1:
                        {
                            ForestArea();
                            break;
                        }
                    case 2:
                        {
                            TownArea();
                            break;
                        }
                    case 3:
                        {
                            MountainArea();
                            break;
                        }
                    case 4:
                        {
                            BossCastle();
                            break;
                        }

                    default:
                        break;
            }
        }

        public void BattleSystem(Player player, Enemy enemy)
        {
            do
            {
                Console.WriteLine("What will you do?");
                Console.WriteLine("1. Attack");

                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        player.DealDamage(enemy);
                        break;
                    default:
                        Console.WriteLine("Invalid action. Please choose again.");
                        continue;
                }
                if (enemy.CurrentHP > 0)
                {
                    enemy.DealDamage(player);
                }

            } while (player.CurrentHP > 0 && enemy.CurrentHP > 0);
        }

        public void ForestArea()
        {

            Console.WriteLine("You have entered the Forest Area.");

        }

        public void TownArea()
        {
            Shop shop = new Shop();
            Console.WriteLine("You have entered the Town Area.");
            shop.ShopMenu(player);

        }

        public void MountainArea()
        {
            Console.WriteLine("You have entered the Mountain Area.");
        }

        public void BossCastle()
        {
            Console.WriteLine("You have entered the Boss' Castle.");
        }

    }
}
