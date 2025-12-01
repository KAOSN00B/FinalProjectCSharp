using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Weapon : Inventory
    {
        public Weapon(string name,  int attackBonus ) : base(name, 0, attackBonus, 0)
        {
            Type = ItemType.Weapon;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Item:{Name} Attack Boost: {AttackBonus}");
        }
    }
}
