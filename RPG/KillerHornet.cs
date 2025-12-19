using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class KillerHornet : Enemy
    {
        public KillerHornet() : base("Killer Hornet", 8, 8, 2, 4, 2, 6)
        {
            DropTable.Add(new Weapon("Hornet Stinger Dagger", 2, 5));
        }
        public override void UseSpecialAbility(Character target)
        {
            Console.WriteLine($"{Name} stings {target.Name} with a venomous attack!");

            int damage = (Attack + 2) - target.Defense;
            if (damage < 0) damage = 0;

            target.Damage(damage);

            Console.WriteLine($"{Name} deals {damage} damage!");

            if (target.CurrentStatus == StatusEffect.Poison)
            {
                Console.WriteLine($"{target.Name} is already poisoned!");
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
