using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Weapon : Inventory
    {
        public Weapon(string name,  int attackBonus, int price ) : base(name, 0, attackBonus, 0, price)
        {
            Type = ItemType.Weapon;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Item:{Name} Attack Boost: {AttackBonus} || Item Type: {Type}");
        }
    }
}
