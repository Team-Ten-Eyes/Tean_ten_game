using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    // What data store will be used.  
    public enum RoundHealingEnum
    {
        Unknown = 0,
        Healing_on= 1, // this popuates the potionList to have 6 potions per round
        Healing_off = 2 //off will only give one healting potion
    }
}
