using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Enemy: Character
    {
        private int xPReward;

        public int XPReward { get { return xPReward; } private set { xPReward = value; } }
        public Enemy(string name, int currentHP, int baseMaxHP, int baseAttack, int baseDefense, int xPReward) : base(name, currentHP, baseMaxHP, baseAttack, baseDefense)
        {
            XPReward = xPReward;
        }

        public override void DealDamage(Character target)
        {
            if (this.CurrentHP <= 0)
            {
                return;
            }

                base.DealDamage(target);

            if (target is Player player && player.CurrentHP <= 0)
            {
                Console.WriteLine($"{player.Name} has been knockedout. {Name} wins\n");
                Console.WriteLine("GameOver: You have been defeated!");
                return;
            }
        }

    public static class EnemyDatabase
    {
        public static List<Enemy> ForestEnemies = new()
        {
            new Enemy("Forest Wolf", 18, 18, 6, 2, 3),
            new Enemy("Goblin Scout", 22, 22, 5, 3, 4),
            new Enemy("Giant Hornet", 14, 14, 7, 1, 2)
        };

        public static List<Enemy> MountainEnemies = new()
        {
            new Enemy("Mountain Bear", 40, 40, 10, 5, 10),
            new Enemy("Rock Golem", 50, 50, 8, 10, 12)
        };
    }


    }
}
