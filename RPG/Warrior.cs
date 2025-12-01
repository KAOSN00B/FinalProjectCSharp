using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPG
{
    public class Warrior : Player
    {
        public Warrior() : base() { }

        public override void SetBaseStats()
        {
            Level = 1;
            XP = 0;
            BaseMaxHP = 20;            
            CurrentHP = MaxHP;
            BaseAttack = 8;
            BaseDefense = 2;
        }
    }
}
