using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;
namespace Game.Helpers
{
    /// <summary>
    /// Helper for the Class Enum Class
    /// </summary>
    static class CharacterTypeEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Attribute
        /// Removes the attributes that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetListCharacterType
        {
            get
            {
                var myList = Enum.GetNames(typeof(CharacterTypeEnum)).ToList();
                return myList;
            }
        }
         
        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CharacterTypeEnum ConvertStringToEnum(string value)
        {
            return (CharacterTypeEnum)Enum.Parse(typeof(AttributeEnum), value);
        }
    }
}