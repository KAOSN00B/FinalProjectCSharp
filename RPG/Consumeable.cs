using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Consumable : Inventory
    {
        private int skillPointBonus;
        public int SkillPointBonus { get { return skillPointBonus; } set { skillPointBonus = value; } }
        public Consumable(string name, int hpBonus, int attackBonus, int defenseBonus, int price, int skillPointBonus ) 
            : base(name, hpBonus, attackBonus, defenseBonus, price)
        {
            SkillPointBonus = skillPointBonus;
            Type = ItemType.Consumable;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Item:{Name} | HP Boost: {HPBonus} | Attack Boost: {AttackBonus} " +
                $"| Defense Boost: {DefenseBonus} | Item Type: {Type} |Skill Point Boost: {skillPointBonus}");
        }
    }
}
