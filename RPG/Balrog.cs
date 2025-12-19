using RPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class Balrog : Enemy
{
    public Balrog()
        : base("Balrog", 14, 20, 4, 7, 2, 4)
    {
        DropTable.Add(new Weapon("Firebrand Greatsword", 8, 5));
        DropTable.Add(new Armour("Balrog Hide", 10, 3, 10));
    }

    public override void UseSpecialAbility(Character target)
    {
        Console.WriteLine($"{Name} unleashes a surge of lightning and a mighty roar at {target.Name}!");

        int damage = (Attack + 3) - target.Defense;
        if (damage < 0) damage = 0;

        target.Damage(damage);

        Console.WriteLine($"{target.Name} takes {damage} damage from the lightning strik!");

        if (target.CurrentStatus == StatusEffect.Confused)
        {
            Console.WriteLine($"{target.Name} is already confused!");
            return;
        }

        target.CurrentStatus = StatusEffect.Confused;
        target.StatusTurns = 2;

        Console.WriteLine($"{target.Name} is confused for 2 turns!");
    }
}

