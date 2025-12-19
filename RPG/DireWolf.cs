using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class DireWolf : Enemy
    {
        public DireWolf() : base("Dire Wolf", 10, 10, 3, 3, 3, 7)
        {

        }

        public override void UseSpecialAbility(Character target)
        {
            Console.WriteLine($"{Name} lunges at {target.Name} with a ferocious bite!");
            int damage = (Attack + 2) - target.Defense;

            if (damage < 0) damage = 0;

            target.Damage(damage);

            Console.WriteLine($"{Name} deals {damage} damage to {target.Name} with Bite!");
            if (target.CurrentHP <= 0)
            {
                target.CurrentHP = 0;
            }
        }
    }
}
