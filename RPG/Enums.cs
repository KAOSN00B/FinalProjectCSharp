using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{

    public enum ItemType 
    { 
        None,
        Consumable, 
        Armour,
        Weapon,
        
    }

    public enum Location
    {
        None,
        Town,
        Forest,
        Mountain,
        BossCastle
    }

    public enum CharacterClass
    {
        None,
        Warrior,
        Mage,
        Rogue
    }

    public enum StatusEffect
    {
        None,
        Poison,
        Burn,
        Stun,
    }


}
