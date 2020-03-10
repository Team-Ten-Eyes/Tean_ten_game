using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    class PotionsModel
    {
        //this is the potions type
        public PotionsEnum PotionType {  get; set; } = PotionsEnum.Mana;

        //this it the amount of health or mana the potion would give to a character
        public uint potionType { get; set; } = 1;
    }
}
