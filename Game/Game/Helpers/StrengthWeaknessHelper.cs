using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Helpers
{
    class StrengthWeaknessHelper
    {

        /// <summary>
        /// The purpose of this functino is to return a string of types that the 
        /// characters have strengths against for the character read page. If an 
        /// invalid type was inputed it would just return the full monster list
        /// but it should never output that if it does there is an error
        /// </summary>
        public static List<string> getCharacterStrengths(string characterType)
        {

            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();

            if (characterType == "Bravery")
            {
                var BraveStrengths = monsterType.Where(a =>
                      a.ToString() != MonsterTypeEnum.Paranoia.ToString() &&
                      a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
                      a.ToString() != MonsterTypeEnum.Anger.ToString() &&
                      a.ToString() != MonsterTypeEnum.Insanity.ToString() &&
                      a.ToString() != MonsterTypeEnum.Depression.ToString()).ToList();


                return BraveStrengths;

            }

            if (characterType == "Creativity")
            {
                var CreativeStrengths = monsterType.Where(a =>
               a.ToString() != MonsterTypeEnum.Anxiety.ToString() &&
               a.ToString() != MonsterTypeEnum.Unknown.ToString() &&
               a.ToString() != MonsterTypeEnum.Depression.ToString() &&
               a.ToString() != MonsterTypeEnum.Insanity.ToString() &&
               a.ToString() != MonsterTypeEnum.BurnOut.ToString()
                    ).ToList();
                return CreativeStrengths;
            }

            if (characterType == "Cunning")
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

        public static List<string> GetCharacterWeaknesses(string characterType)
        {
            var monsterType = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();

            if (characterType == "Bravery")
            {
                var BraveStrengths = monsterType.Where(a =>
                      a.ToString() == MonsterTypeEnum.Paranoia.ToString() &&
                      a.ToString() == MonsterTypeEnum.Anger.ToString() &&
                      a.ToString() == MonsterTypeEnum.Depression.ToString()).ToList();
                return BraveStrengths;

            }

            if (characterType == "Creativity")
            {
                var CreativeStrengths = monsterType.Where(a =>
               a.ToString() == MonsterTypeEnum.Anxiety.ToString() &&
               a.ToString() == MonsterTypeEnum.Depression.ToString() &&
               a.ToString() == MonsterTypeEnum.BurnOut.ToString()).ToList();
                return CreativeStrengths;
            }

            if (characterType == "Cunning")
            {
                var CunningStrengths = monsterType.Where(a =>
               a.ToString() == MonsterTypeEnum.Paranoia.ToString() &&
               a.ToString() == MonsterTypeEnum.Fear.ToString()).ToList();
                return CunningStrengths;
            }

            return monsterType;

        }

        /// <summary>
        /// This is the monster counterpart for the character get strengths. This will reutrn the 
        /// list of the monsters strengths
        /// </summary>
        /// <param name="monsterType"></param>
        /// <returns></returns>
        public static List<string> getMonsterStrengths(string monsterType)
        {
            var characterType = Enum.GetNames(typeof(CharacterTypeEnum)).ToList();

            if (monsterType == "Depression")
            {
                var DStrengths = characterType.Where(a =>
            a.ToString() != CharacterTypeEnum.Unknown.ToString() &&
           a.ToString() != CharacterTypeEnum.Cunning.ToString()).ToList();
                return DStrengths;
            }

            if (monsterType == "Paranoia")
            {
                var PStrengths = characterType.Where(a =>
               a.ToString() != CharacterTypeEnum.Creativity.ToString() &&
               a.ToString() != CharacterTypeEnum.Unknown.ToString()).ToList();
                return PStrengths;
            }

            if (monsterType == "Anger")
            {
                var AngerStrengths = characterType.Where(a =>
                a.ToString() == CharacterTypeEnum.Bravery.ToString()).ToList();
                return AngerStrengths;
            }

            if (monsterType == "Fear")
            {
                var fearStrenths = characterType.Where(a =>
               a.ToString() == CharacterTypeEnum.Cunning.ToString()).ToList();
                return fearStrenths;
            }

            if (monsterType == "Anxiety")
            {
                var AnxietyStrengths = characterType.Where(a =>
               a.ToString() == CharacterTypeEnum.Creativity.ToString()).ToList();
                return AnxietyStrengths;
            }
            if (monsterType == "BurnOut")
            {
                var burnoutStrengths = characterType.Where(a =>
               a.ToString() == CharacterTypeEnum.Creativity.ToString()).ToList();
                return burnoutStrengths;
            }

            if (monsterType == "Stress")
            {
                List<string> nothing = new List<string>();
                nothing.Add("none");
                return nothing;
            }
            return characterType;

        }
    }
}
