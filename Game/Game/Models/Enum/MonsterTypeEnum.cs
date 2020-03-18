namespace Game.Models
{
    /// <summary>
    /// List of different Monster classes
    public enum MonsterTypeEnum
    {
        Unknown = 0,
        Stress = 1,
        Paranoia = 2,
        Anxiety = 3,
        Fear = 4,
        Depression = 5,
        Anger = 6,
        BurnOut = 7,
        Insanity = 8, //I feel this one should be the final boss
    }

    /// <summary>
    /// string conversion of the enum
    /// </summary>
    public static class MonsterTypeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this MonsterTypeEnum value)
        {
            // Default String
            var Message = "Monster";

            switch (value)
            {
                case MonsterTypeEnum.Stress:
                    Message = "Stress";
                    break;

                case MonsterTypeEnum.Paranoia:
                    Message = "Paranoia";
                    break;

                case MonsterTypeEnum.Anxiety:
                    Message = "Anxiety";
                    break;

                case MonsterTypeEnum.Fear:
                    Message = "Fear";
                    break;

                case MonsterTypeEnum.Depression:
                    Message = "Depression";
                    break;

                case MonsterTypeEnum.Anger:
                    Message = "Anger";
                    break;

                case MonsterTypeEnum.BurnOut:
                    Message = "Burnout";
                    break;

                case MonsterTypeEnum.Insanity:
                    Message = "Insanity";
                    break;

                case MonsterTypeEnum.Unknown:
                    break;

            }

            return Message;
        }

    }
}