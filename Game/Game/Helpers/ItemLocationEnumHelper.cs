using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;

namespace Game.Helpers
{
    class ItemLocationEnumHelper
    {
        /// <summary>
        /// Gets the list of locations that an Item can have.
        /// Does not include the Left and Right Finger 
        /// </summary>
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(ItemLocationEnum)).ToList();
                var myReturn = myList.Where(a =>
                                            a.ToString() != ItemLocationEnum.Unknown.ToString() &&
                                            a.ToString() != ItemLocationEnum.LeftFinger.ToString() &&
                                            a.ToString() != ItemLocationEnum.RightFinger.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();
                return myReturn;
            }
        }

        /// <summary>
        ///  Gets the list of locations a character can use
        ///  Removes Finger for example, and allows for left and right finger
        /// </summary>
        public static List<string> GetListCharacter
        {
            get
            {
                var myList = Enum.GetNames(typeof(ItemLocationEnum)).ToList();
                var myReturn = myList.Where(a =>
                                           a.ToString() != ItemLocationEnum.Unknown.ToString() &&
                                            a.ToString() != ItemLocationEnum.Finger.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();

                return myReturn;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ItemLocationEnum ConvertStringToEnum(string value)
        {
            return (ItemLocationEnum)Enum.Parse(typeof(ItemLocationEnum), value);
        }

        /// <summary>
        /// If asked for a position number, return a location.  Head as 1 etc. 
        /// This compsenstates for the enum #s not being sequential, but allows for calls to the postion for random allocation etc (roll 1-7 dice and pick a item to equipt), etc... 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static ItemLocationEnum GetLocationByPosition(int position)
        {
            switch (position)
            {
                case 1:
                    return ItemLocationEnum.Head;

                case 2:
                    return ItemLocationEnum.Necklass;

                case 3:
                    return ItemLocationEnum.PrimaryHand;

                case 4:
                    return ItemLocationEnum.OffHand;

                case 5:
                    return ItemLocationEnum.RightFinger;

                case 6:
                    return ItemLocationEnum.LeftFinger;

                case 7:
                default:
                    return ItemLocationEnum.Feet;
            }
        }
    }
}