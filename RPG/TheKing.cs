using RPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class TheKing : Enemy
{
    public TheKing()
        : base("The King", 40, 40, 7, 10, 4, 7)
    { 
    }

    public override void UseSpecialAbility(Character target)
    {
        Random rng = new Random();
        int choice = rng.Next(1, 6); 

        Console.WriteLine($"{Name} uses Royal Command!");

        switch (choice)
        {
            // 1. Massive damage
            case 1:
                int heavyDamage = (Attack + 6) - target.Defense;
                if (heavyDamage < 0) heavyDamage = 0;

                target.Damage(heavyDamage);

                Console.WriteLine($"{Name} strikes with overwhelming force, dealing {heavyDamage} damage!");
                break;

            // 2. Stun attack
            case 2:
                Console.WriteLine($"{Name} bellows a deafening roar!");

                if (target.CurrentStatus == StatusEffect.Stun)
                {
                    Console.WriteLine($"{target.Name} already afflicted with {target.CurrentStatus} ");
                    break;
                }

                target.CurrentStatus = StatusEffect.Stun;
                target.StatusTurns = 1;

                Console.WriteLine($"{target.Name} is paralyzed with fear!");
                break;

            // 3. Confuse player
            case 3:
                Console.WriteLine($"{Name} distorts {target.Name}'s senses with dark magic!");

                if (target.CurrentStatus == StatusEffect.Confused)
                {
                    Console.WriteLine($"{target.Name} already afflicted with {target.CurrentStatus} ");
                    break;
                }

                target.CurrentStatus = StatusEffect.Confused;
                target.StatusTurns = 3;

                Console.WriteLine($"{target.Name} becomes confused for 3 turns!");
                break;

            // 4. Heal himself
            case 4:
                int healAmount = 8;
                CurrentHP += healAmount;
                if (CurrentHP > MaxHP) CurrentHP = MaxHP;

                Console.WriteLine($"{Name} heals himself for {healAmount} HP!");
                break;

            // Poisons the player
            case 5:
                Console.WriteLine($"{Name} thows a poison dagger at {target.Name}!");

                if (target.CurrentStatus == StatusEffect.Poison)
                {
                    Console.WriteLine($"{target.Name} already afflicted with {target.CurrentStatus} ");
                    break;
                }

                target.CurrentStatus = StatusEffect.Poison;
                target.StatusTurns = 3;

                Console.WriteLine($"{target.Name} is paralyzed with fear!");
                break;

        }
    }
}

