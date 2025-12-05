using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Character
    {
        private string name;
        private int currentHP;
        private int maxHP;
  
        private int baseAttack;
        private int baseDefense;
        private int baseMaxHP;

        public StatusEffect CurrentStatus { get; set; } = StatusEffect.None;
        public int StatusTurns { get; set; } = 0;
        public int StatusDamage { get; set; } = 0;

        public string Name { get { return name; } set { name = value; } }
        public int CurrentHP { get { return currentHP; } set { currentHP = value; } }
        public int BaseAttack { get { return baseAttack; } set { baseAttack = value; } }
        public int BaseDefense { get { return baseDefense; } set { baseDefense = value; } }
        public int BaseMaxHP { get { return baseMaxHP; } set { baseMaxHP = value; } }

        public Weapon? EquippedWeapon { get; protected set; }
        public Armour? EquippedArmour { get; protected set; }    

        // Calculated, NEVER stored
        public int Attack => BaseAttack + (EquippedWeapon?.AttackBonus ?? 0);
        public int Defense => BaseDefense + (EquippedArmour?.DefenseBonus ?? 0);
        public int MaxHP => BaseMaxHP + (EquippedArmour?.HPBonus ?? 0);
        

        public Character(string name, int currentHP, int baseMaxHP, int baseAttack, int baseDefense)
        {
            Name = name;
            CurrentHP = currentHP;
            BaseMaxHP = baseMaxHP;
            BaseAttack = baseAttack;
            BaseDefense = BaseDefense;
        }

        public Character() : this("", 0, 0, 0, 0) { }  

        public virtual void DisplayStats()
        {
            Console.WriteLine($"Name: {Name} | HP: Current HP:{CurrentHP}/Max HP:{MaxHP} | Attack Power: {Attack} | Defense: {Defense} | ");
        }

        public void Damage(int damage)
        {
            CurrentHP -= damage;
        }

        public virtual void DealDamage(Character target)
        {
            int damage = Attack - target.Defense;

            if (this.CurrentHP <= 0)
            {
                return;
            }

            if (damage < 0) damage = 0;

            target.Damage(damage);

            if (target.CurrentHP > 0)
            {
                Console.WriteLine($"{target.name} has {target.CurrentHP} remaining\n");

            }

        }

        public virtual void UseSpecialAbility(Character target)
        {
            Console.WriteLine($"{Name} uses their special ability!");
        }
    }
}
