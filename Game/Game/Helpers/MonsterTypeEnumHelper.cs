﻿using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Game.Helpers
{
    /// <summary>
    /// Helper for the Class Enum Class
    /// </summary>
    public static class MonsterTypeEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Monster Type
        /// </summary>
        public static List<string> GetListMonsterTypeAll
        {
            get
            {
                var myList = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of the enums types without unknown
        /// </summary>
        public static List<string> GetListMonsterType
        {
            get
            {
                var myList = Enum.GetNames(typeof(MonsterTypeEnum)).ToList().Where(m => m.ToString().Equals("Unknown") == false).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MonsterTypeEnum ConvertStringToEnum(string value)
        {
            return (MonsterTypeEnum)Enum.Parse(typeof(MonsterTypeEnum), value);
        }
    }
}