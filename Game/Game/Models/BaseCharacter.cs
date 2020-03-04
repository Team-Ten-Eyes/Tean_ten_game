﻿using System;
using System.Collections.Generic;
using System.Text;
using Game.Helpers;

namespace Game.Models
{
    public class BaseCharacter : PlayerModel<BaseCharacter>
    {


        // Enum of the different "CLass" that the character has
        public CharacterTypeEnum Attribute { get; set; } = CharacterTypeEnum.Bravery; //defaults to bravery


        //Current Mana, Mana is used for special attacks, this feature will be added later
        public uint Mana { get; set; } = 0;

        public uint MaxMana { get; set; } = 0;
        //Used to calculate ToHit roll

        //Items held by a character
        public List<ItemModel> HeldItems;

        new public int GetAttack()
        {
            return 1;

        }




        ////////////////////////////////////////////////////

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public BaseCharacter()
        {

            PlayerType = PlayerTypeEnum.Character;
            
            Name = "Default";
            ImageURI = "wizard_avatar.png";
            Level = 1;
            CurrHealth = 10;
            MaxHealth = 10;
            Mana = 5;
            MaxMana = 5;
            Experience = 1;
            Attack = 5;
            Defense = 5;
            Speed = 3;
        }

        /// <summary>
        /// Copy Constructor to create an item based on what is passed in
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
        public override bool Update(BaseCharacter newData)
        {
            if (newData == null)
            {
                return false;
            }

            // Update all the fields in the Data, except for the Id and guid
            PlayerType = newData.PlayerType;

            Name = newData.Name;
            Guid = newData.Guid;
            Level = newData.Level;
            Experience = newData.Experience;
            Attribute = newData.Attribute;
            CurrHealth = newData.CurrHealth;
            Attack = newData.Attack;
            Speed = newData.Speed;
            Defense = newData.Defense;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            return true;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description + " for " +
                            Attribute.ToString() +
                            "+" + Attack + " , " +
                            "Experience : " + Experience + " , " +
                            "Level : " + Level;

            return myReturn.Trim();
        }



        //public int GetAttack() { }
        //Used to determine the Class of a character
        public CharacterTypeEnum GetCharType()
        {
            return Attribute;
        }

        //Used to reduce mana points after a special attack is used
        public bool TakeMana(uint mana)
        {
            Mana -= mana;
            return true;
        }
        //Called when some special item is used
        public bool AddMana(uint mana)
        {
            if (mana + Mana > MaxMana)
                Mana = MaxMana;
            else Mana += mana;
            return true;
        }
        //Called on level up
        public void AddMaxMana()
        {
            Mana = MaxMana;
        }

    }
}
