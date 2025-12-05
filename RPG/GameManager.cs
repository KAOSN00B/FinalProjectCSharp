using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static RPG.Enemy;

namespace RPG
{
    public class GameManager
    {
        static Random rng = new Random();
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
            new Armour("Cloth Robe", 1, 3),
        };

        public GameManager() { }

        public void StartGame()
        {
            Console.WriteLine("Welcome to the RPG Game!");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Load Game");
            string choice = Console.ReadLine();

            while (true)
            {
                if (choice == "1")
                {
                    NewGame();
                }
                else if (choice == "2")
                {
                    // LoadGame(); // Implement load game functionality
                    Console.WriteLine("Load Game functionality not yet implemented.");
                    StartGame();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    StartGame();
                }
            }

        }
        public void NewGame()
        {
            Console.WriteLine("Please make your character.");
            player = Player.CharacterCreator();
            MainMenu(player);
        }

        public void MainMenu(Player player)
        {
            Console.WriteLine("\nOptions");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. World Selection");
            Console.WriteLine("3. Equip Inventory ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    NewGame();
                    break;
                case "2":
                    WorldSelection(player);
                    break;
                case "3":
                    EquipGear(player);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    MainMenu(player);
                    break;
            }
        }

        public void WorldSelection(Player player)
        {
            while (true)
            {
                Console.WriteLine("\nWhere would you like to go?");
                Console.WriteLine("1. Forest Area");
                Console.WriteLine("2. Town Area");
                Console.WriteLine("3. Mountain Area");
                Console.WriteLine("4. Boss' Castle");
                Console.WriteLine("5. View Inventory");
                Console.WriteLine("6. Return to Main Menu");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        player.CurrentLocation = Location.Forest;
                        ForestArea(player);
                        break;

                    case "2":
                        player.CurrentLocation = Location.Town;
                        TownArea(player);
                        break;

                    case "3":
                        player.CurrentLocation = Location.Mountain;
                        MountainArea(player);
                        break;

                    case "4":
                        player.CurrentLocation = Location.BossCastle;
                        BossCastle(player);
                        break;
                    case "5":
                        player.DisplayInventory();
                        break;
                    case "6":
                        EquipGear(player);
                        break; 
                    case "0":
                        MainMenu(player);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.\n");
                        break;
                }
            }
        }


        public void BattleSystem(Player player, Enemy enemy)
        {
            while (player.CurrentHP > 0 && enemy.CurrentHP > 0)
            {
                Console.WriteLine("\nWhat will you do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use Skill");
                Console.WriteLine("3. View Stats (Free Action)");
                Console.WriteLine("4. View Enemy Stats (Free Action)");
                Console.WriteLine("5. Equip Gear(Will use a turn)");
                Console.WriteLine("0. Flee");

                string input = Console.ReadLine();

                bool usedTurn = true; 

                switch (input)
                {
                    case "1":
                        player.DealDamage(enemy);
                        break;
                    case "2":
                        player.UseSpecialAbility(enemy);
                        break;
                    case "3":
                        player.DisplayStats();
                        usedTurn = false;  
                        break;
                    case "4":
                        enemy.DisplayStats();
                        usedTurn = false;  
                        break;
                    case "5":
                        EquipGear(player);
                        break;
                    case "0":
                        Console.WriteLine("You fled the battle!");
                        return;
                    default:
                        Console.WriteLine("Invalid action.");
                        usedTurn = false;   
                        break;
                }

                
                if (enemy.CurrentStatus != StatusEffect.None)
                {
                    ApplyStatusEffect(enemy);
                }

                if (!usedTurn)
                    continue;

                if (enemy.CurrentHP > 0)
                {   
                    enemy.UseSpecialAbility(player);
                }
                if (player.CurrentStatus != StatusEffect.None)
                {
                    ApplyStatusEffect(player);
                }


            }

            if (player.CurrentHP > 0 && enemy.CurrentHP <= 0)
            {
                player.GainXP(enemy);
                player.Gold += enemy.GoldReward;
                Console.WriteLine($"You get {enemy.GoldReward} gold. \n" +
                    $"You now have {player.Gold} gold. ");
            }
        }


        public void ForestArea(Player player)
        {
            DireWolf direWolf = new DireWolf();
            KillerHornet killerHornet = new KillerHornet();

            var forestEnemies = new List<Enemy>()
            {
                direWolf,
                killerHornet
            };

            Console.WriteLine("You have entered the Forest Area.");

            int encounterChance = rng.Next(1, 101);

            if (encounterChance <= 50)
            {

                Enemy enemy = forestEnemies[rng.Next(forestEnemies.Count)];

                Console.WriteLine($"A wild {enemy.Name} appears!");
                BattleSystem(player, enemy);
            }
            else
            {
                Console.WriteLine("The forest is peaceful. No enemies encountered.");
            }
            Console.WriteLine("What would you like to do?");
            Console.Write("Enter your choice: ");
            Console.WriteLine("Explore");
            Console.WriteLine("Leave");

            string choice = Console.ReadLine();

            if (choice.ToLower() == "Explore")
            {
                ForestArea(player);
            }
            else if (choice.ToLower() == "Leave")
            {
                WorldSelection(player);
            }

        }


        public void TownArea(Player player)
        {
            player.CurrentHP = player.MaxHP;
            Console.WriteLine("You visited the Town and you feel fully refreshed!");
            Console.WriteLine($"Your HP is now {player.CurrentHP}/{player.MaxHP}.");

            while (true)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Visit Shop");
                Console.WriteLine("2. Return to World Selection");
                Console.WriteLine("3. Talk to a local");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Shop shop = new Shop();
                        shop.ShopMenu(player);
                        break;

                    case 2:
                        WorldSelection(player);
                        return; 

                    case 3:
                        NPC npc = new NPC();
                        npc.Speak();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }


        public void MountainArea(Player player)
        {
            Console.WriteLine("You have entered the Mountain Area.");
            int encounterChance = rng.Next(1, 101);

            if (encounterChance <= 70)
            {
               //Add new enemies for this area
                
            }
            else
            {
                Console.WriteLine("The Mountain is peaceful. No enemies encountered.");
            }
        }

        public void BossCastle(Player player)
        {
            Console.WriteLine("You have entered the Boss' Castle.");
        }

        public void EquipGear(Player player)
        {
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty. No gear to equip.");
                MainMenu(player);
            }

            while (true)
            {
                foreach (var item in player.Inventory)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Enter the name of the gear you want to equip:");
                string gearName = Console.ReadLine();

                var gearToEquip = player.Inventory.FirstOrDefault(i => i.Name.Equals(gearName, StringComparison.OrdinalIgnoreCase));

                if (gearToEquip == null)
                {
                    Console.WriteLine("Invalid Selection");

                }
                else
                {
                    if (gearToEquip is Weapon weapon)
                    {
                        player.EquipWeapon(weapon);
                        Console.WriteLine($"{weapon.Name} equipped.");
                    }
                    else if (gearToEquip is Armour armour)
                    {
                        player.EquipArmour(armour);
                        Console.WriteLine($"{armour.Name} equipped.");
                    }
                    break;
                }
                return;

            }
        }

        void ApplyStatusEffect(Character target)
        {
            if (target.CurrentStatus == StatusEffect.None)
                return;

            switch (target.CurrentStatus)
            {
                case StatusEffect.Poison:
                    target.CurrentHP -= target.StatusDamage;
                    Console.WriteLine($"{target.Name} suffers {target.StatusDamage} poison damage!");
                    break;

                case StatusEffect.Burn:
                    target.CurrentHP -= target.StatusDamage;
                    Console.WriteLine($"{target.Name} takes {target.StatusDamage} burn damage!");
                    break;

                case StatusEffect.Stun:
                    Console.WriteLine($"{target.Name} is stunned and cannot move!");
                    // skip their turn logic handled elsewhere
                    break;
            }

            target.StatusTurns--;

            if (target.CurrentHP <= 0)
            {
                target.CurrentHP = 0;
            }

            if (target.StatusTurns <= 0)
            {
                Console.WriteLine($"{target.Name} is no longer affected by {target.CurrentStatus}.");
                target.CurrentStatus = StatusEffect.None;
                target.StatusDamage = 0;
            }
        }
        


    }
}
