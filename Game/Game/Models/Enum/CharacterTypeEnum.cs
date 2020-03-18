namespace Game.Models
{
    /// <summary>
    /// List of different Character classes
    /// </summary>
    public enum CharacterTypeEnum
    {
        Unknown = 0,
        Bravery = 1,
        Creativity = 2,
        Cunning = 3,
    }

    /// <summary>
    /// string conversion of the enum
    /// </summary>
    public static class CharacterTypeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterTypeEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case CharacterTypeEnum.Bravery:
                    Message = "Bravery";
                    break;

                case CharacterTypeEnum.Creativity:
                    Message = "Creativity";
                    break;

                case CharacterTypeEnum.Cunning:
                    Message = "Cunning";
                    break;

                case CharacterTypeEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }

    }
}