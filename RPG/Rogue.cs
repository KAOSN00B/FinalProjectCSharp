using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Rogue : Player
    {
        public Rogue() : base() { }

        public override void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            BaseMaxHP = 16;
            CurrentHP = MaxHP;
            BaseAttack = 6;
            BaseDefense = 1;
            SkillPoints = 3;
            Class = CharacterClass.Rogue;
        }

        public override void UseSpecialAbility(Character target)
        {
            Console.WriteLine($"{Name} Throws a poisonous dagger at {target.Name}");

            int damage = (Attack + 6) - target.Defense;
            if (damage < 0) damage = 0;

            target.Damage(damage);

            Console.WriteLine($"{Name} deals {damage} damage!");

            if (target.CurrentStatus == StatusEffect.Poison)
            {
                Console.WriteLine($"{target.Name} is already poisoned!");
                return;
            }

            if (target is TheKing)
            {
                Console.WriteLine("The King is immune to poison!");
                return;
            }

            // Apply poison
            target.CurrentStatus = StatusEffect.Poison;
            target.StatusTurns = 3;
            target.StatusDamage = 2;

            Console.WriteLine($"{target.Name} has been poisoned for 3 turns!");
        }
    }
}
