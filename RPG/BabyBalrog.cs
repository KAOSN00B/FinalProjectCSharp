using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class BabyBalrog : Enemy
    {
        public BabyBalrog() : base("Baby Balrog", 20, 20, 7, 5, 6, 4)
        {
            DropTable.Add(new Weapon("Small Fire Sword", 4, 5));
            DropTable.Add(new Armour("Light Balrog Hide", 6, 3, 6));
            DropTable.Add(new Consumable("Ember Fragment", 10, 0, 0, 0, 3));

        }

        public override void UseSpecialAbility(Character target)
        {
            Console.WriteLine($"{Name} Screams at the top of it's lungs at rattling {target.Name} brain and confusing them.");

            if (target.CurrentStatus == StatusEffect.Confused)
            {
                Console.WriteLine($"{target.Name} is already confused!");
                return;
            }

            target.CurrentStatus = StatusEffect.Confused;
            target.StatusTurns = 2;

            Console.WriteLine($"{target.Name} is confused for 2 turns!");
        }
    }
}
