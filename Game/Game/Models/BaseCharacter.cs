 using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    class BaseCharacter : DefaultModel
    {
        public BaseCharacter()
        {

            

        }

        public bool update(BaseCharacter charData)
        {
            Name = charData.Name;
            return true;
        }

    }
}
