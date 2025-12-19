using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Mage : Player
    {
        public Mage() : base() { }

        public override void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            BaseMaxHP = 15;
            CurrentHP = MaxHP;
            BaseAttack = 3;
            BaseDefense = 2;
            SkillPoints = 3;
            Class = CharacterClass.Mage;
        }

        public override void UseSpecialAbility(Character target)
        {

            int damage = (Attack) - target.Defense;
            if (damage < 0) damage = 0;

            target.Damage(damage);

            Console.WriteLine($"{Name} Shoots a bold of lightning at {target.Name} ");
            Console.WriteLine($"{Name} deals {damage} damage!");

            if (target.CurrentStatus == StatusEffect.Stun)
            {
                Console.WriteLine($"{target.Name} is already Stunned!");
                return;
            }

            if (target is TheKing)
            {
                Console.WriteLine($"{target.Name} is immune to Stun!");
                return;
            }

            target.CurrentStatus = StatusEffect.Stun;
            target.StatusTurns = 2;

            Console.WriteLine($"{target.Name} has been stunned for 2 Turn");
        }
    }
}
