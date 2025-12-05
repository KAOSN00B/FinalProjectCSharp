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
            BaseAttack = 1;
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
            SkillPoints -= 1;
            return;
        }
    }
}
