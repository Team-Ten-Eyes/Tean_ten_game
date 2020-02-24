using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using System.Linq;

namespace Game.Helpers
{
    class StrengthWeaknessHelper
    {

        /// <summary>
        /// The purpose of this functino is to return a string of types that the 
        /// characters have strengths against for the character read page. If an 
        /// invalid type was inputed it would just return the full monster list
        /// </summary>
        public static List<string> getCharacterStrengths (string characterType)
        {
            
                var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();

            if(characterType == "Bravery")
            {
                var BraveStrengths = monsterType.Where(a =>
                      a.ToString() != MonsterTypeEnum.Paranoia.ToString() &&
                      a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
                      a.ToString() != MonsterTypeEnum.Anger.ToString() &&
                      a.ToString() != MonsterTypeEnum.Insanity.ToString()&&
                      a.ToString() != MonsterTypeEnum.Depresion.ToString()).ToList();
                       

                return BraveStrengths;

            }

            if(characterType == "Creativity")
            {
                var CreativeStrengths = monsterType.Where(a =>
               a.ToString() != MonsterTypeEnum.Anxiety.ToString() &&
               a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
               a.ToString() != MonsterTypeEnum.Depresion.ToString() &&
               a.ToString() != MonsterTypeEnum.Insanity.ToString() &&
               a.ToString() != MonsterTypeEnum.BurnOut.ToString()
                    ).ToList();
                return CreativeStrengths;
            }

            if( characterType == "Cunning")
            {
                var CunningStrengths = monsterType.Where(a =>
               a.ToString() != MonsterTypeEnum.Paranoia.ToString() &&
               a.ToString() != MonsterTypeEnum.Insanity.ToString() &&
               a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
               a.ToString() != MonsterTypeEnum.Fear.ToString()).ToList();
                return CunningStrengths;
            }

            return monsterType;
            
        }
    }
}
