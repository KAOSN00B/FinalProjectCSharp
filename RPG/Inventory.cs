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
        private int price;

        public string Name { get { return name; } private set { name = value; } }
        public int HPBonus { get { return hpBonus; } protected set { hpBonus = value; } }
        public int AttackBonus { get { return attackBonus; } protected set { attackBonus = value; } }
        public int DefenseBonus { get { return defenseBonus; } protected set { defenseBonus = value; } }
        public int Price { get { return price; } protected set { price = value; } }

        protected Inventory(string name, int hpBonus, int attackBonus, int defenseBonus, int price)
        {
            Name = name;
            HPBonus = hpBonus;
            AttackBonus = attackBonus;
            DefenseBonus = defenseBonus;
            Price = price;
            Type = ItemType.None;
        }

        public abstract void DisplayInfo();

        public Inventory Copy()
        {
            return (Inventory)this.MemberwiseClone();
        }
    }
}

