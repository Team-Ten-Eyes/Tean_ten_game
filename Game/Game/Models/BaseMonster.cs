using System;
using System.Collections.Generic;
using System.Text;
using Game.ViewModels;
using Game.Helpers;
namespace Game.Models
{
    public class BaseMonster : PlayerModel<BaseMonster>
    {


        /// </summary>
        // Enum of the different class that the monster modifies
        public MonsterTypeEnum Attribute { get; set; } = MonsterTypeEnum.Stress; //defaults to stress


        // characer stats//////////////////////////////////
        //current health



        ////////////////////////////////////////////////////

        // Special items dropped by a monster on battleend
        //Default constructor

        public BaseMonster()
        {
            PlayerType = PlayerTypeEnum.Monster;
            Name = "Default_Bad";
            ImageURI = "Insanty.png";
            Level = 1;
            CurrHealth = 10;
            MaxHealth = 10;
            Attack = 3;
            Defense = 3;
            Speed = 3;
            Description = "Stress monster";
            ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;
            Difficulty = DifficultyEnum.Average;
        }

        /// <summary>
        /// Copy Constructor to create an item based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public BaseMonster(BaseMonster data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override bool Update(BaseMonster newData)
        {
            if (newData == null)
            {
                return false;
            }


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

            // Update all the fields in the Data, except for the Id and guid
            
            return true;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description + " for " +
                            Attribute.ToString() +
                            "+" + Attack + " , " +
                            "Level : " + Level;

            return myReturn.Trim();
        }



        public string GetMonsterType()
        {
            return Attribute.ToString();
        }


        public int GetAttack()
        {
            return 20;
        }

        //TO BE IMPLEMENTED WITH BATTLE SYSTEM
        //public int GetDamageDice(){}
        //public int GetDamageRollValue(){}
        //public ItemModel RollItemDrop(){}


    }
}

