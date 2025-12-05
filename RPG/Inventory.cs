using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Inventory
    {
        public ItemType Type { get; protected set; }

        private string name;
        private int hpBonus;
        private int attackBonus;
        private int defenseBonus;

        public string Name { get { return name; } private set { name = value; } }
        public int HPBonus { get { return hpBonus; }  set { hpBonus = value; } }
        public int AttackBonus { get { return attackBonus; } private set { attackBonus = value; } }
        public int DefenseBonus { get { return defenseBonus; } private set { defenseBonus = value; } }

        public Inventory(string name, int hpBonus, int attackBonus, int defenseBonus)
        {
            Name = name;
            HPBonus = hpBonus;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
            Type = ItemType.None;
        }

        public abstract void DisplayInfo();


    }
}
