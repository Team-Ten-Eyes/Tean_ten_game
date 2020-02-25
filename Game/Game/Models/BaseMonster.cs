using System;
using System.Collections.Generic;
using System.Text;

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
            Name = "Default_Bad";
            ImageURI = "Insanty.png";
            Level = 1;
            CurrHealth = 10;
            MaxHealth = 10;
            Attack = 3;
            Defense = 3;
            Speed = 3;
            Description = "Stress monster";
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
        public override void Update(BaseMonster newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Level = newData.Level;
            Attribute = newData.Attribute;
            Attack = newData.Attack;
            Defense = newData.Defense;
            Speed = newData.Speed;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
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


        public override int GetDamageRollValue()
        {
            return 20;

        }

        //TO BE IMPLEMENTED WITH BATTLE SYSTEM
        //public int GetDamageDice(){}
        //public int GetDamageRollValue(){}
        //public ItemModel RollItemDrop(){}


    }
}

