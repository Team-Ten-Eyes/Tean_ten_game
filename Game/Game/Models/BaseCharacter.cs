using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class BaseCharacter : BaseModel<BaseCharacter>
    {


        //  character level info//////////////////////////////
        public int level { get; set; } = 1;

        public int Experience { get; set; } = 0;
        /// //////////////////////////////////////////
        /// 
       
        /// </summary>
        // Enum of the different attributes that the character modifies, Items can only modify one character
        public CharacterTypeEnum Attribute { get; set; } = CharacterTypeEnum.Bravery; //defaults to bravery


        // characer stats//////////////////////////////////
        public int CharHealth { get; set; } = 0;

        public int MaxHealth { get; set; } = 0;

        public int Mana { get; set; } = 0;

        public int MaxMana { get; set; } = 0;

        public int Attack { get; set; } = 0;

        public int defense { get; set; } = 0;

        public int speed { get; set; } = 0;
        ////////////////////////////////////////////////////

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public BaseCharacter()
        {
            Name = "Default";
            ImageURI = "knight.png";
            level = 7;
            CharHealth = 15;
            MaxHealth = 15;
            Mana = 5;
            MaxMana = 10;
            Experience = 5;
            Attack = 15;
            defense = 10;
            speed = 2;
        }

        /// <summary>
        /// Constructor to create an item based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public BaseCharacter(BaseCharacter data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override void Update(BaseCharacter newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            level = newData.level;
            Experience = newData.Experience;
            Attribute = newData.Attribute;
            CharHealth = newData.CharHealth;
            Attack = newData.Attack;
            defense = newData.defense;  
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
                            "Experience : " + Experience + " , " +
                            "Level : " + level;

            return myReturn.Trim();
        }


    }
}
