using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;

namespace Game.Helpers
{
    /// <summary>
    /// Helper for the Difficulty Enum Class
    /// </summary>
    public static class DifficultyEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Difficulty
        /// Removes the Difficultys that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetListAll
        {
            get
            {
                var myList = Enum.GetNames(typeof(DifficultyEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Difficulty
        /// Removes the unknown
        /// </summary>
        public static List<string> GetListMonster
        {
            get
            {
                var myList = Enum.GetNames(typeof(DifficultyEnum)).ToList().Where(m => m.ToString().Equals("Unknown") == false).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DifficultyEnum ConvertStringToEnum(string value)
        {
            return (DifficultyEnum)Enum.Parse(typeof(DifficultyEnum), value);
        }
    }
}