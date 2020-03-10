using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    
        /// <summary>
        /// This is the enum for the potions model that would determine 
        /// potions functionality 
        /// </summary>
        public enum PotionsEnum
        {
            // Not specified
            Unknown = 0,

            Health = 1,

            Mana = 2,


        }

        /// <summary>
        /// Friendly strings for the Enum Class
        /// </summary>
        public static class PotionsEnumExtensions
        {
            /// <summary>
            /// Display a string for the Enums
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static string ToMessage(this PotionsEnum value)
            {
                //default string
                var Message = "unknown";

                switch (value)
                {
                    case PotionsEnum.Health:
                        Message = "Health";
                        break;
                    case PotionsEnum.Mana:
                        Message = "Mana";
                        break;
                }
                return Message;
            }
        }
    
    
}
