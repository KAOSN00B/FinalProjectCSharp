using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Armour : Inventory
    {
        public Armour(string name, int hpBonus,  int defenseBonus) : base(name, hpBonus, 0, defenseBonus)
        {
            Type = ItemType.Armour;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Item:{Name} | HP Boost: {HPBonus} | Defense Boost {DefenseBonus}");
        }
    }
}
