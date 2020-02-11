using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    class AttackOption
    {
        public string Name { get; set; } = "PUNCH!";
        public int AttackBuff { get; set; } = 1;//Added to attack stats when an attack is made 
    }
}
