using RPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;



namespace RPG
{
    public class Player : Character
    {
        public List<Inventory> Inventory { get; private set; } = new List<Inventory>();

        private string hairColour;
        private string gender;
        private int age;
        private int level;
        private int xp;
        private int xpRequired;
        private int skillPoints;
        private int maxSkillPoints;
        private int gold;
        public Location CurrentLocation { get; set; }

        public CharacterClass Class { get; protected set; }

        public string HairColour { get { return hairColour; } private set { hairColour = value; } }
        public string Gender { get { return gender; } private set { gender = value; } }
        public int Age { get { return age; } private set { age = value; } }
        public int Level { get { return level; } set { level = value; } }
        public int XP { get { return xp; } set { xp = value; } }
        public int XPRequired { get { return xpRequired; } set { xpRequired = value; } }
        public int SkillPoints { get { return skillPoints; }  set { skillPoints = value; } }
        public int Gold { get { return gold; } set { gold = value; } }

        public Player() : base() { }

        public virtual void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            XPRequired = 10;
            BaseMaxHP = 20;
            CurrentHP = MaxHP;
            BaseAttack = 3;
            BaseDefense = 3;
            SkillPoints = 3;
            Gold = 0;
            CurrentLocation = Location.None;
            Class = CharacterClass.None;

        }

        public static Player CharacterCreator()
        {

            Player? player = null;

            while (player == null)
            {
                Console.WriteLine("Choose your class:");
                Console.WriteLine("1) Warrior");
                Console.WriteLine("2) Rogue");
                Console.WriteLine("3) Mage");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                player = choice switch
                {
                    "1" => new Warrior(),
                    "2" => new Rogue(),
                    "3" => new Mage(),
                    _ => null
                };

                if (player == null)
                {
                    Console.WriteLine("Invalid choice. Please try again.\n");
                }
            }


            Console.WriteLine("Name?");
            player.Name = Console.ReadLine();

            Console.WriteLine("Hair Colour?");
            player.HairColour = Console.ReadLine();

            Console.WriteLine("Gender?");
            player.Gender = Console.ReadLine();

            int age;

            while (true)
            {
                Console.WriteLine("Age?");

                if (int.TryParse(Console.ReadLine(), out age) && age > 0)
                {
                    player.Age = age;
                    break; 
                }

                Console.WriteLine("Invalid age. Please enter a valid number.\n");
            }

            player.SetBaseStats();

            player.DisplayStats();
            Console.WriteLine($"Hair Colour: {player.HairColour} | Gender: {player.Gender} | Age: {player.Age}");

            return player;
        }

        public void AboutPlayer()
        {
            Console.WriteLine($"Name: {Name} | Hair Colour: {HairColour}" +
                $"Age | {Age}"); 
        }

        public override void DisplayStats()
        {
            Console.WriteLine($"Name: {Name} | Level: {Level} |Skill Points: {SkillPoints}");
            base.DisplayStats();
  
            if (Inventory.Count() < 1)
            {
                Console.WriteLine("Inventory: (empty)");
            }
            else
            {
                Console.WriteLine("Inventory:");
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"- {item.Name}");
                }
            }

        }

        public override void DealDamage(Character target)
        {
            if (this.CurrentHP <= 0)
            {
                return;
            }

            base.DealDamage(target);

        }

        public void GainXP(Enemy expAmount)
        {
            XP += expAmount.XPReward;

            Console.WriteLine($"{Name} gained {expAmount.XPReward} XP. Total XP: {XP}");

            if (XP >= XPRequired)
            {
                LevelUp();
                XPRequired += 5;
                XP = 0;
            }
        }

        public void LevelUP()
        {
            Level++;
            BaseMaxHP += 3;
            BaseAttack += 3;
            BaseDefense += 3;

        }


        public void EquipWeapon(Weapon newWeapon)
        {
            
            // Add to inventory if not already there
            if (!Inventory.Contains(newWeapon))
            {
                Inventory.Add(newWeapon);
            }

            // Equip the weapon
            EquippedWeapon = newWeapon;

            Console.WriteLine($"{Name} equipped: {newWeapon.Name} (+{newWeapon.AttackBonus} ATK)");
        }


        public void EquipArmour(Armour newArmour)
        {
            if (!Inventory.Contains(newArmour))
            {
                Inventory.Add(newArmour);
            }

            EquippedArmour = newArmour;

            Console.WriteLine($"{Name} equipped: {newArmour.Name} " +
                $"(+Defense: {newArmour.DefenseBonus}, HP Bonus: +{newArmour.HPBonus})");
        }

        public void RemoveWeapon(Weapon equippedWeapon)
        {
            if (EquippedWeapon == null)
            {
                Console.WriteLine("No weapon currently equipped.");
                return;
            }
            Console.WriteLine($"{Name} unequipped {EquippedWeapon.Name}.");
            EquippedWeapon = null;
        }

        public void RemoveArmour(Armour equippedarmour)
        {
            if (EquippedArmour == null)
            {
                Console.WriteLine("No Armour currently equipped.");
                return;
            }
            Console.WriteLine($"{Name} unequipped {EquippedArmour.Name}.");
            EquippedArmour = null;
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Inventory:");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("  (empty)");
                return;
            }
            foreach (var item in Inventory)
            {
                item.DisplayInfo();
            }
            return;
        }

        public void LevelUp()
        {
            if (XP > 1)
            {
                XP = 0;
                Console.WriteLine($"{Name} has leveled up!");
            }
            Level++;
            BaseMaxHP += 5;
            BaseAttack += 2;
            BaseDefense += 2;
            CurrentHP = MaxHP;
            Console.WriteLine($"Congratulations! {Name} has reached Level {Level}!");
            DisplayStats();
        }

        public void UseItem(Player player)
        {

            var consumables = player.Inventory
                .Where(i => i.Type == ItemType.Consumable)
                .ToList();

            if (consumables.Count == 0)
            {
                Console.WriteLine("You have no consumable items to use.");
                return;
            }

            Console.WriteLine("Consumable Items:");
            foreach (var item in consumables)
            {
                Console.WriteLine($"- {item.Name} (HP: +{item.HPBonus}, ATK: +{item.AttackBonus}, DEF: +{item.DefenseBonus})");
            }

            Console.WriteLine("\nEnter the name of the item you want to use:");
            string itemName = Console.ReadLine();

            // Find the item
            var itemToUse = consumables
                .FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (itemToUse == null)
            {
                Console.WriteLine("Invalid item name.");
                return;
            }

            player.CurrentHP += itemToUse.HPBonus;
            player.BaseAttack += itemToUse.AttackBonus;
            player.BaseDefense += itemToUse.DefenseBonus;

            if (player.CurrentHP > player.MaxHP)
                player.CurrentHP = player.MaxHP;

            Console.WriteLine($"{itemToUse.Name} used!");

            player.Inventory.Remove(itemToUse);
        }



    }
}
