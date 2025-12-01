using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Rogue : Player
    {
        public Rogue() : base() { }

        public override void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            BaseMaxHP = 16;
            CurrentHP = MaxHP;
            BaseAttack = 6;
            BaseDefense = 1;
        }
    }
}
