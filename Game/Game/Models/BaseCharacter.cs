using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class BaseCharacter : BaseModel<BaseCharacter>
    {
        //  character level info//////////////////////////////
        public uint Level { get; set; } = 1;

        public uint Experience { get; set; } = 0;
        /// //////////////////////////////////////////
        /// 

        /// </summary>
        // Enum of the different attributes that the character modifies, Items can only modify one character
        public CharacterTypeEnum Attribute { get; set; } = CharacterTypeEnum.Bravery; //defaults to bravery


        // characer stats//////////////////////////////////
        public uint CharHealth { get; set; } = 0;

        public uint MaxHealth { get; set; } = 0;

        public uint Mana { get; set; } = 0;

        public uint MaxMana { get; set; } = 0;

        public uint Attack { get; set; } = 0;

        public uint Defense { get; set; } = 0;

        public uint Speed { get; set; } = 0;

        public List<ItemModel> HeldItems;

        public bool IsAlive = true;
        ////////////////////////////////////////////////////

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public BaseCharacter()
        {
            Name = "Default";
            ImageURI = "wizard_avatar.png";
            Level = 1;
            CharHealth = 10;
            MaxHealth = 10;
            Mana = 5;
            MaxMana = 5;
            Experience = 1;
            Attack = 5;
            Defense = 5;
            Speed = 3;
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
            Level = newData.Level;
            Experience = newData.Experience;
            Attribute = newData.Attribute;
            CharHealth = newData.CharHealth;
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

        public bool LevelUp() {
            return true;
        }

        public bool AddExperience(uint toAdd) {
            Experience += toAdd;
            return true;
        }

        public bool TakeDamage(uint Damage) {
            CharHealth -= Damage;
            if (CharHealth <= 0)
                IsAlive = false;
            return true;
        }

        //public int GetAttack() { }

        public CharacterTypeEnum GetCharType(){
            return Attribute;
        }

        public bool TakeMana(uint mana) {
            Mana -= mana;
            return true;
        }

        public bool AddMana(uint mana) {
            if (mana + Mana > MaxMana)
                Mana = MaxMana;
            else Mana += mana;
            return true;
        }

        public void AddMaxMana() {
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
