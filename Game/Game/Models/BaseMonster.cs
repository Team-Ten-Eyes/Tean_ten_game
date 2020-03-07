namespace Game.Models
{
    public class BaseMonster : PlayerModel<BaseMonster>
    {

        public MonsterTypeEnum Attribute { get; set; } = MonsterTypeEnum.Anger;



        /// <summary>
        /// Set Type to Monster
        /// 
        /// Set Name and Description
        /// </summary>
        public BaseMonster()
        {
            Attribute = MonsterTypeEnum.Anxiety;
            PlayerType = PlayerTypeEnum.Monster;
            Guid = Id;
            Name = "EVOLVE MONSTER";
            Description = "Issa Kraken";
            Attack = 1;
            Difficulty = DifficultyEnum.Average;
            UniqueItem = null;
            ImageURI = "Fire_monster.png";
            Experience = 0;
            ExperienceRemaining = Helpers.LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;
        }

        /// <summary>
        /// Make a copy
        /// </summary>
        /// <param name="data"></param>
        public BaseMonster(BaseMonster data)
        {
            Update(data);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public override bool Update(BaseMonster newData)
        {
            if (newData == null)
            {
                return false;
            }
            Attribute = newData.Attribute;
            PlayerType = newData.PlayerType;
            Guid = newData.Guid;
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ImageURI = newData.ImageURI;

            Difficulty = newData.Difficulty;

            Speed = newData.Speed;
            Defense = newData.Defense;
            Attack = newData.Attack;

            Experience = newData.Experience;
            ExperienceRemaining = newData.ExperienceRemaining;
            CurrHealth = newData.CurrHealth;
            MaxHealth = newData.MaxHealth;

            Head = newData.Head;
            Necklass = newData.Necklass;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
            UniqueItem = newData.UniqueItem;

            return true;
        }

        /// <summary>
        /// Helper to show the Specific Monster Type/Class/Attribute when needed
        /// </summary>
        /// <returns></returns>
        public MonsterTypeEnum GetMonsterType()
        {
            return Attribute;
        }



        /// <summary>
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public override string FormatOutput()
        {
            var myReturn = Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();
            myReturn += " , Difficulty : " + Difficulty.ToString();
            myReturn += " , Total Experience : " + Experience;
            myReturn += " , Items : " + ItemSlotsFormatOutput();
            myReturn += " , Damage : " + GetDamageTotalString;

            return myReturn;
        }
    }
}