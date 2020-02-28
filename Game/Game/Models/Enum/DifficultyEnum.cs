using System;

namespace Game.Models
{
    /// <summary>
    /// The Types of Difficultys
    /// Used by Item to specify what it modifies.
    /// </summary>
    public enum DifficultyEnum
    {
        // Not specified
        Unknown = 0,

        // Easier than mostThe speed of the character, impacts movement, and initiative
        Easy = 10,

        // Average
        Average = 12,

        // Hard
        Hard = 14,

        // Harder than Hard
        Difficult = 16,

        // The highest value
        Impossible = 18,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class DifficultyEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this DifficultyEnum value)
        {
            // Default String
            var Message = "Unknown";

            switch (value)
            {
                case DifficultyEnum.Easy:
                    Message = "Easy";
                    break;

                case DifficultyEnum.Average:
                    Message = "Average";
                    break;

                case DifficultyEnum.Hard:
                    Message = "Hard";
                    break;

                case DifficultyEnum.Difficult:
                    Message = "Harder than Hard";
                    break;

                case DifficultyEnum.Impossible:
                    Message = "Impossible";
                    break;

                case DifficultyEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }

        /// <summary>
        /// Return a modifier as an Int rounded up
        /// 
        /// From the Enun value
        /// 
        /// And the passed in value
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToModifier(this DifficultyEnum EnumValue, int value)
        {
            // Default String
            var modifier = 1.0;

            switch (EnumValue)
            {
                case DifficultyEnum.Easy:
                    modifier = .25;
                    break;

                case DifficultyEnum.Average:
                    modifier = .5;
                    break;

                case DifficultyEnum.Hard:
                    modifier = 1;
                    break;

                case DifficultyEnum.Difficult:
                    modifier = 1.5;
                    break;

                case DifficultyEnum.Impossible:
                    modifier = 2;
                    break;

                case DifficultyEnum.Unknown:
                default:
                    break;
            }

            var MaxHealthAdjusted = Convert.ToInt32(Math.Ceiling(value * modifier));

            return MaxHealthAdjusted;
        }
    }
}