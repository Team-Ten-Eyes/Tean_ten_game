using System;
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

     

        

        ////////////////////////////////////////////////////

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public BaseCharacter()
        {
            Guid = Id;
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
        public override void Update(BaseCharacter newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Level = newData.Level;
            Experience = newData.Experience;
            Attribute = newData.Attribute;
            CurrHealth = newData.CurrHealth;
            Attack = newData.Attack;
            Speed = newData.Speed;
            Defense = newData.Defense;
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
                            "Level : " + Level;

            return myReturn.Trim();
        }
        //Method to be called when a character levels up, not implemented
        public bool LevelUp()
        {
            return true;
        }
        //Adds experience upon a hit of a monster, not implemented
        public bool AddExperience(int toAdd)
        {
            Experience += toAdd;
            return true;
        }
        //Method to remove HP from a character and returns if a Char is dead


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


 

        //TO BE IMPLEMENTED WITH BATTLE SYSTEM
        //public int GetDamageDice(){}
        //public int GetDamageRollValue(){}
        //public List<ItemModel> DropAllItems(){}
        //public ItemModel GetItemByLocation(enum Location){}
        //public bool AddItem(ItemModel item, Location location){}
        //public int GetItemBonusAtk(){}
        //public int GetItemBonusDef(){}
        //public int GetItemBonusSpd(){}

    }
}
