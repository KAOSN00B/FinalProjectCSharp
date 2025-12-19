using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Golem : Enemy
    {
        public Golem() : base("Golem", 16, 16, 4, 8, 6, 6)
        {
            DropTable.Add(new Consumable("Potion", 5, 0, 0, 0, 3));
            DropTable.Add(new Armour("Rock ChestPlate", 2, 1, 2));
        }

        public override void UseSpecialAbility(Character target)
        {

            int damage = (Attack) - target.Defense;
            if (damage < 0) damage = 0;

            target.Damage(damage);

            Console.WriteLine($"{Name} Stuns {target.Name} with terrifying earthquake");
            Console.WriteLine($"{Name} deals {damage} damage!");

            if (target.CurrentStatus == StatusEffect.Stun)
            {
                Console.WriteLine($"{target.Name} is already Stunned!");
                return;
            }

            target.CurrentStatus = StatusEffect.Stun;
            target.StatusTurns = 1;

            Console.WriteLine($"{target.Name} has been stunned for 1 Turn");
        }
    }
}
