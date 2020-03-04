
using Game.Helpers;

namespace Game.Models
{
    /// <summary>
    /// Player for the game.
    /// 
    /// Either Monster or Character
    /// 
    /// Constructor Player to Player used a T in Round
    /// </summary>
    public class PlayerInfoModel : PlayerModel<PlayerInfoModel>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PlayerInfoModel() { }

        /// <summary>
        /// Copy from one PlayerInfoModel into Another
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(PlayerInfoModel data)
        {
            
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            MaxHealth = data.GetMaxHealthTotal;
            CurrHealth = data.GetMaxHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;
            UniqueItem = data.UniqueItem;

            Difficulty = data.Difficulty;
        }

        /// <summary>
        /// Create PlayerInfoModel from character
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(BaseCharacter data)
        {
            
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            MaxHealth = data.GetMaxHealthTotal;
            CurrHealth = data.GetMaxHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            UniqueItem = data.UniqueItem;

            Difficulty = data.Difficulty;

            // Give the copy a differet quid, so it can be used in the battles as a copy
            Guid = System.Guid.NewGuid().ToString();

            // Set current experience to be 1 above minimum.
            Experience = LevelTableHelper.Instance.LevelDetailsList[Level - 1].Experience + 1;
        }

        /// <summary>
        /// Crate PlayerInfoModel from Monster
        /// </summary>
        /// <param name="data"></param>
        public PlayerInfoModel(BaseMonster data)
        {
            PlayerType = data.PlayerType;
            Guid = data.Guid;
            Alive = data.Alive;
            Experience = data.Experience;
            ExperienceRemaining = data.ExperienceRemaining;
            Level = data.Level;
            Name = data.Name;
            Description = data.Description;
            Speed = data.GetSpeed();
            ImageURI = data.ImageURI;
            MaxHealth = data.GetMaxHealthTotal;
            CurrHealth = data.GetCurrentHealthTotal;

            // Set the strings for the items
            Head = data.Head;
            Feet = data.Feet;
            Necklass = data.Necklass;
            RightFinger = data.RightFinger;
            LeftFinger = data.LeftFinger;
            Feet = data.Feet;

            UniqueItem = data.UniqueItem;

            Difficulty = data.Difficulty;

            // Give the copy a differet quid, so it can be used in the battles as a copy
            Guid = System.Guid.NewGuid().ToString();

            // Set amount to give to be 1 below max for that level.
            ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;
        }

        public override string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();

            if (PlayerType == PlayerTypeEnum.Character)
            {
                myReturn += " , Total Experience : " + Experience;
                myReturn += " , Damage : " + GetDamageTotalString;
                myReturn += " , Attack :" + GetAttackTotal;
                myReturn += " , Defense :" + GetDefenseTotal;
                myReturn += " , Speed :" + GetSpeedTotal;
            }

            myReturn += " , Items : " + ItemSlotsFormatOutput();

            return myReturn;
        }
    }
}