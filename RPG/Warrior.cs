using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPG
{
    public class Warrior : Player
    {
        public Warrior() : base() { }

        public override void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            BaseMaxHP = 20;            
            CurrentHP = MaxHP;
            BaseAttack = 5;
            BaseDefense = 2;
            SkillPoints = 3;
            Class = CharacterClass.Warrior;
        }

        public override void UseSpecialAbility(Character target)
        {
            Console.WriteLine($"{Name} Screms: AHHHHHHHHHHHHH!!!!");

            int damage = (Attack * 2) - target.Defense;
            if (damage < 0) damage = 0;

            target.Damage(damage);

            Console.WriteLine($"{Name} deals {damage} damage to {target.Name} with Fierce Smash!");

            if (target.CurrentHP <= 0)
            {
                target.CurrentHP = 0; 
            }

            if (target.CurrentStatus == StatusEffect.Confused)
            {
                Console.WriteLine($"{target.Name} is already confused!");
                return;
            }

            if (target is TheKing)
            {
                Console.WriteLine("The King is immune to poison!");
                return;
            }

            target.CurrentStatus = StatusEffect.Confused;
            target.StatusTurns = 2;

            SkillPoints -= 1;
            return;
        }
    }
}
