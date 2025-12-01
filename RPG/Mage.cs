using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Mage : Player
    {
        public Mage() : base() { }

        public override void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            BaseMaxHP = 15;
            CurrentHP = MaxHP;
            BaseAttack = 3;
            BaseDefense = 2;
        }
    }
}
