using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Enemy: Character
    {
        private int xPReward;
        private int goldReward;
        public List<Inventory> DropTable { get; set; } = new List<Inventory>();

        public int XPReward { get { return xPReward; }  set { xPReward = value; } }
        public int GoldReward { get { return goldReward; }  set { goldReward = value; } }
        public Enemy(string name, int currentHP, int baseMaxHP, int baseAttack, int baseDefense, int xPReward, int goldReward) : base(name, currentHP, baseMaxHP, baseAttack, baseDefense)
        {
            XPReward = xPReward;
            GoldReward = goldReward;
            
        }

        public abstract override void UseSpecialAbility(Character target);


    }
}
