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


        public string HairColour { get { return hairColour; } private set { hairColour = value; } }
        public string Gender { get { return gender; } private set { gender = value; } }
        public int Age { get { return age; } private set { age = value; } }
        public int Level { get { return level; } set { level = value; } }
        public int XP { get { return xp; } set { xp = value; } }

        public Player() : base() { }

        public virtual void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            BaseMaxHP = 20;
            CurrentHP = MaxHP;
            BaseAttack = 3;
            BaseDefense = 3;
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

            Console.WriteLine("Age?");
            player.Age = int.Parse(Console.ReadLine());

            player.level = 1;
            player.BaseMaxHP = 20;
            player.CurrentHP = player.MaxHP;
            player.BaseAttack = 3;
            player.BaseDefense = 3;

            player.SetBaseStats();

            player.DisplayStats();
            Console.WriteLine($"Hair Colour: {player.HairColour} | Gender: {player.Gender} | Age: {player.Age}");

            return player;
        }

        public override void DealDamage(Character target)
        {
            if (this.CurrentHP <= 0)
            {
                return;
            }


            base.DealDamage(target);

            if (target is Enemy enemy && enemy.CurrentHP <= 0)
            {
                GainXP(enemy);
            }

        }

        public void GainXP(Enemy expAmount)
        {
            XP += expAmount.XPReward;

            Console.WriteLine($"{Name} gained {expAmount.XPReward} XP. Total XP: {XP}");
            // Check for level up
            if (XP >= 1)
            {
                LevelUp();

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



    }
}
