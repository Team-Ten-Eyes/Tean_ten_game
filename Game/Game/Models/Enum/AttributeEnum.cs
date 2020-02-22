namespace Game.Models
{
    /// <summary>
    /// The Types of Attributes
    /// Used by Item to specify what it modifies.
    /// </summary>
    public enum AttributeEnum
    {
        // Not specified
        Unknown = 0,    

        // The speed of the character, impacts movement, and initiative
        Speed = 10,

        // The defense score, to be used for defending against attacks
        Defense = 12,

        // The Attack score to be used when attacking
        Attack = 14,

        // Current Health which is always at or below MaxHealth
        CurrentHealth = 16,

        // The highest value health can go
        MaxHealth = 18,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class AttributeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this AttributeEnum value)
        {
            // Default String
            var Message = "Unknown";

            switch (value)
            {
                case AttributeEnum.Attack:
                    Message = "Attack";
                    break;

                case AttributeEnum.CurrentHealth:
                    Message = "Current Health";
                    break;

                case AttributeEnum.Defense:
                    Message = "Defense";
                    break;

                case AttributeEnum.MaxHealth:
                    Message = "Max Health";
                    break;

                case AttributeEnum.Speed:
                    Message = "Speed";
                    break;

                case AttributeEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }
}