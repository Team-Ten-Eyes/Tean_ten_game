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
    /// Dispay a string for the enums
    /// </summary>
    public static class characterTypeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterTypeEnum value)
        {
            var Message = "Bravery";

            switch (value)
            {
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