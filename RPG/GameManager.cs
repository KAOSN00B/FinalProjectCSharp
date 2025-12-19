using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            new Weapon("Iron Sword", 5, 5),
            new Weapon("Bronze Dagger", 3, 5),
            new Weapon("Oak Staff", 2, 5),
        };

        public List<Armour> AllArmour = new List<Armour>
          {
            new Armour("Leather Vest", 3, 3, 3),
            new Armour("Chainmail", 5, 3, 5),
            new Armour("Cloth Robe", 1, 3, 5),
        };

        public GameManager() { }

        public void StartGame()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Castle Of Darkness ===");

                Console.WriteLine("Menu: ");
                Console.WriteLine("1) New Game");
                Console.WriteLine("2) Load Game");
                Console.WriteLine("3) Delete Save");
                Console.WriteLine("0) Exit");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        NewGame();
                        return; // exit start menu, game begins

                    case "2":
                        {
                            var loaded = LoadMenu();
                            if (loaded != null)
                            {
                                player = loaded;
                                Console.WriteLine($"Welcome back {player.Name}!");
                                Console.ReadLine();
                                WorldSelection(player);

                                return; // 🔥 THIS IS THE KEY LINE
                            }

                            Console.WriteLine("Load cancelled.");
                            Console.ReadLine();
                            break;
                        }


                    case "3":
                        DeleteSaveMenu();
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void NewGame()
        {
            Console.WriteLine("Please tell me about yourself.");
            player = Player.CharacterCreator();
            MainMenu(player);
        }

        public void MainMenu(Player player)
        {
            Console.WriteLine("\nOptions");
            Console.WriteLine("1. World Selection");
            Console.WriteLine("2. SaveGame");
            Console.WriteLine("3. Load Game");
            Console.WriteLine("4. Title Screen");
            
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    WorldSelection(player);
                    break;
                case "2":
                    if (player != null)
                        SaveMenu(player);
                    break;
                case "3":
                    {
                        var loaded = LoadMenu();
                        if (loaded != null)
                        {
                            player = loaded;
                            Console.WriteLine($"Welcome back {player.Name}!");
                            WorldSelection(player);
                            return; // exit StartGame FOREVER
                        }

                        Console.WriteLine("Load cancelled.");
                        Console.ReadLine();
                        break;
                    }

                case "4":
                    StartGame();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    MainMenu(player);
                    break;
            }
        }

        public void WorldSelection(Player player)
        {
            if (player.RealmOfDarknessKey)
            {
                Console.WriteLine("\n\nThe Realm of Darkness is now accessible!");

            }
            while (true)
            {
                Console.WriteLine("\nWhere would you like to go?");
                Console.WriteLine("1. Forest Area");
                Console.WriteLine("2. Town Area");
                Console.WriteLine("3. Mountain Area");
                Console.WriteLine("4. Boss' Castle");
                Console.WriteLine("5. View Inventory");
                Console.WriteLine("6. Return to Main Menu");

                if (player.RealmOfDarknessKey)
                {
                    Console.WriteLine("7. Realm of Darkness");
                }

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
                        KingsCastle(player);
                        break;

                    case "5":
                        EquipOrRemoveGear(player);
                        break;

                    case "6":
                        MainMenu(player);
                        return;

                    case "7":
                        if (player.RealmOfDarknessKey)
                        {
                            player.CurrentLocation = Location.RealmOfDarkness;
                            RealmOfDarkness(player);
                        }
                        else
                        {
                            Console.WriteLine("That location is not available yet.");
                        }
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

                if (player.CurrentHP <= 0) break;

                Console.WriteLine("\n\nWhat will you do?");
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
                        EquipOrRemoveGear(player);
                        break;
                    case "0":
                        Console.WriteLine("You fled the battle!");
                        WorldSelection(player);
                        return;
                    default:
                        Console.WriteLine("Invalid action.");
                        usedTurn = false;
                        break;
                }

                if (!usedTurn)
                    continue;

                if (enemy.CurrentHP <= 0) break; // alive check before anything like poison (in case of error)

                // ENEMY STATUS EFFECTS
                if (enemy.CurrentStatus != StatusEffect.None)
                    ApplyStatusEffect(enemy);

                if (enemy.CurrentHP <= 0) break; // checks if enemy has died from status effect before their turn

                // ENEMY TURN ACTION

                int roll = rng.Next(1, 101);

                if (roll <= 30)
                {
                    enemy.UseSpecialAbility(player);
                }
                else
                {
                    enemy.DealDamage(player);

                }

                if (player.CurrentStatus != StatusEffect.None)
                    ApplyStatusEffect(player);

                if (player.CurrentHP <= 0)
                {
                    GameOver(player);
                    return;
                }
            }

            if (player.CurrentHP > 0 && enemy.CurrentHP <= 0)
            {
                player.GainXP(enemy);
                player.Gold += enemy.GoldReward;
                Console.WriteLine($"You get {enemy.GoldReward} gold. \n" +
                    $"You now have {player.Gold} gold. ");
                SpawnDrop(player, enemy);
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

            Console.WriteLine("\n\nYou have entered the Forest Area.");

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
            Console.WriteLine("Enter your choice: ");
            Console.WriteLine("1. Explore");
            Console.WriteLine("2. Equip Gear");
            Console.WriteLine("3. Leave");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                ForestArea(player);
            }
            else if (choice.ToLower() == "2")
            {
                EquipOrRemoveGear(player);

            }
            else if (choice.ToLower() == "3")
            {
                WorldSelection(player);
            }

        }

        public void GameOver(Player player)
        {
            while (true) 
            {
            Console.WriteLine("\n\nYou have been defeated!");
            Console.WriteLine("Game Over.");

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Spawn Back at world map");
            Console.WriteLine("2. New Game");
            Console.WriteLine("3. Load Game");
            Console.WriteLine("4. Exit Game");

            string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        player.CurrentHP = player.MaxHP;
                        Console.WriteLine("You have been revived and returned to the world map.");
                        WorldSelection(player);
                        break;
                    case "2":
                        NewGame();
                        break;
                    case "3":
                        LoadMenu();
                        StartGame();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }


        public void TownArea(Player player)
        {
            player.CurrentHP = player.MaxHP;
            player.SkillPoints = 3;
            Console.WriteLine("\n\nYou visited the Town and you feel fully refreshed!");
            Console.WriteLine($"Your HP is now {player.CurrentHP}/{player.MaxHP}.");

            while (true)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Visit Shop");
                Console.WriteLine("2. Return to World Selection");
                Console.WriteLine("3. Talk to a local");
                Console.WriteLine("4. Equip/Unequip Gear");

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
                    case 4:
                        EquipOrRemoveGear(player);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }


        public void MountainArea(Player player)
        {
            Golem golem = new Golem();
            BabyBalrog babyBalrog = new BabyBalrog();

            var mountainEnemies = new List<Enemy>()
            {
                golem,
                babyBalrog
            };

            Console.WriteLine("\n\nYou have entered the Forest Area.");

            int encounterChance = rng.Next(1, 101);

            if (encounterChance <= 50)
            {

                Enemy enemy = mountainEnemies[rng.Next(mountainEnemies.Count)];

                Console.WriteLine($"A wild {enemy.Name} appears!");
                BattleSystem(player, enemy);
            }
            else
            {
                Console.WriteLine("The forest is peaceful. No enemies encountered.");
            }
            Console.WriteLine("What would you like to do?");
            Console.Write("Enter your choice: ");
            Console.WriteLine("1. Explore");
            Console.WriteLine("2. Equip/Unequip item.");
            Console.WriteLine("3. Leave");

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

        public void KingsCastle(Player player)
        {
            Console.WriteLine("\n\nYou have entered the King's Castle.");

            DireWolf direWolf = new DireWolf();
            KillerHornet killerHornet = new KillerHornet();
            BabyBalrog babyBalrog = new BabyBalrog();
            Golem golem = new Golem();
            Balrog balrog = new Balrog();

            var castleEnemies = new List<Enemy>()
            {
                direWolf,
                killerHornet,
                babyBalrog,
                golem,
            };

            if (player.RealmOfDarknessKey != true)
            {
                Console.WriteLine("Oh no you set off an alarm. You hear monsters rushing towards you!");
                Console.WriteLine("Prepare for battle!");
                Enemy enemy = castleEnemies[rng.Next(castleEnemies.Count)];
                Console.WriteLine($"{enemy.Name} appears!");
                BattleSystem(player, enemy);

                Console.WriteLine("The second monster has shown up. Becareful!");
                Enemy enemy2 = castleEnemies[rng.Next(castleEnemies.Count)];
                Console.WriteLine($"{enemy2.Name} appears!");
                BattleSystem(player, enemy2);

                Console.WriteLine("You have taken care of the smaller enemies but now the big bad Monster has shown up. Time to finish the fight against the leader of the monsters");
                Console.WriteLine("The Balrog has appeared. Becareful it's massive size is as fightening as it's Power!");
                BattleSystem(player, balrog);

                Console.WriteLine("You hear a voice telling you to go to the realm of darkness to finish what you have started...");
                Console.WriteLine("Then a gemstone appears in front of you. You pick it up and feel a surge of power flow through your body.");
                Console.WriteLine("(The realm of darkness has been uncovered in the world map");

                player.RealmOfDarknessKey = true;
                WorldSelection(player);
            }
            else
            {
                Console.WriteLine("You have already cleared the King's Castle. There is nothing more to do here.");
                WorldSelection(player);
            }


        }

        public void RealmOfDarkness(Player player)
        {
            TheKing theKing = new TheKing();
            Console.WriteLine("\n\nYou have entered the Realm of Darkness.");
            Console.WriteLine("The King of Monsters appears!");
            BattleSystem(player, theKing);
            if (player.CurrentHP > 0)
            {
                Console.WriteLine("Congratulations! You have defeated The King and completed the game!");
                Console.WriteLine($"{player.Name} will be remembered for the rest of time.");

            }

        }

        public void EquipGear(Player player)
        {
            Console.WriteLine();
            player.DisplayStats();
            player.DisplayInventory();
            Console.WriteLine();

            if (player.PlayerInventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty. No gear to equip.");
                MainMenu(player);
            }

            while (true)
            {
                foreach (var item in player.PlayerInventory)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Enter the name of the gear you want to equip:");
                string gearName = Console.ReadLine();

                var gearToEquip = player.PlayerInventory.FirstOrDefault(i => i.Name.Equals(gearName, StringComparison.OrdinalIgnoreCase));

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

        public void EquipOrRemoveGear(Player player)
        {
            if (player.PlayerInventory.Count == 0 &&
                player.EquippedWeapon == null &&
                player.EquippedArmour == null)
            {
                Console.WriteLine("You have no gear to manage.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n--- Manage Gear ---");

                Console.WriteLine($"Weapon: {(player.EquippedWeapon?.Name ?? "None")}");
                Console.WriteLine($"Armour: {(player.EquippedArmour?.Name ?? "None")}");

                Console.WriteLine("\nInventory:");
                player.DisplayInventory();

                Console.WriteLine("\nCommands:");
                Console.WriteLine("Type item name to equip");
                Console.WriteLine("remove weapon");
                Console.WriteLine("remove armour");
                Console.WriteLine("exit");

                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    return;

                if (input.Equals("remove weapon", StringComparison.OrdinalIgnoreCase))
                {
                    // 👇 Uses your existing method
                    player.RemoveWeapon(player.EquippedWeapon);
                    continue;
                }

                if (input.Equals("remove armour", StringComparison.OrdinalIgnoreCase))
                {
                    // 👇 Uses your existing method
                    player.RemoveArmour(player.EquippedArmour);
                    continue;
                }

                var item = player.PlayerInventory
                    .FirstOrDefault(i => i.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

                if (item == null)
                {
                    Console.WriteLine("Invalid command or item.");
                    continue;
                }

                if (item is Weapon weapon)
                {
                    player.EquipWeapon(weapon);
                }
                else if (item is Armour armour)
                {
                    player.EquipArmour(armour);
                }
                else
                {
                    Console.WriteLine("That item cannot be equipped.");
                }
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

                case StatusEffect.Confused:
                    Console.WriteLine($"{target.Name} is confused!");

                    // 30% chance to hit yourself
                    int roll = rng.Next(1, 101);

                    if (roll <= 80)
                    {
                        int confusionDamage = 4;
                        target.CurrentHP -= confusionDamage;
                        Console.WriteLine($"{target.Name} hits themselves in confusion for {confusionDamage} damage!");
                    }
                    else
                    {
                        Console.WriteLine($"{target.Name} resists the confusion this turn.");
                    }
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

        public void SpawnDrop(Player player, Enemy enemy)
        {
            if (enemy.DropTable.Count == 0)
                return;

            // % chance to drop an item
            int roll = GameManager.rng.Next(1, 101);
            if (roll > 50) return;

            // Pick a random item from the table
            Inventory drop = enemy.DropTable[GameManager.rng.Next(enemy.DropTable.Count)];

            Console.WriteLine($"{enemy.Name} dropped: {drop.Name}!");

            player.PlayerInventory.Add(drop);
        }

        public static void SaveMenu(Player player)
        {
            Console.WriteLine("\n--- Save Game ---");
            for (int i = 1; i <= 3; i++)
            {
                string path = $"save_slot_{i}.txt";
                Console.WriteLine($"{i}) Slot {i} {(File.Exists(path) ? "(USED)" : "(EMPTY)")}");
            }

            Console.Write("Choose a slot (1–3) or 0 to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 3)
                return;

            if (choice == 0)
                return;

            player.Save(choice);
            Console.WriteLine($"Game saved to Slot {choice}.");
        }

        public static Player LoadMenu()
        {
            Console.WriteLine("\n--- Load Game ---");
            for (int i = 1; i <= 3; i++)
            {
                string path = $"save_slot_{i}.txt";
                Console.WriteLine($"{i}) Slot {i} {(File.Exists(path) ? "(USED)" : "(EMPTY)")}");
            }

            Console.Write("Choose a slot (1–3) or 0 to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 3)
                return null;

            if (choice == 0)
                return null;

            string file = $"save_slot_{choice}.txt";
            if (!File.Exists(file))
            {
                Console.WriteLine("No save file in that slot.");
                return null;
            }

            Console.WriteLine($"Loaded Slot {choice}.");
            return Player.Load(choice);
        }

        public static void DeleteSaveMenu()
        {
            Console.WriteLine("\n--- Delete Save ---");
            for (int i = 1; i <= 3; i++)
            {
                string path = $"save_slot_{i}.txt";
                Console.WriteLine($"{i}) Slot {i} {(File.Exists(path) ? "(USED)" : "(EMPTY)")}");
            }

            Console.Write("Choose a slot to delete (1–3) or 0 to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 3)
                return;

            if (choice == 0)
                return;

            string file = $"save_slot_{choice}.txt";
            if (!File.Exists(file))
            {
                Console.WriteLine("Slot is already empty.");
                return;
            }

            Console.Write("Are you sure? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                File.Delete(file);
                Console.WriteLine($"Slot {choice} deleted.");
            }
        }





    }
}
