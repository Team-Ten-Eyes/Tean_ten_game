﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class BaseMonster : BaseModel<BaseMonster>
    {


        //  character level info//////////////////////////////
        public int level { get; set; } = 1;


        /// </summary>
        // Enum of the different attributes that the character modifies, Items can only modify one character
        public MonsterTypeEnum Attribute { get; set; } = MonsterTypeEnum.Stress; //defaults to bravery


        // characer stats//////////////////////////////////
        public int MonsterHealth { get; set; } = 0;

        public int MaxHealth { get; set; } = 0;

        public int Attack { get; set; } = 0;

        public int defense { get; set; } = 0;

        public int speed { get; set; } = 0;
        ////////////////////////////////////////////////////

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public BaseMonster()
        {
            Name = "Default_Bad";
            ImageURI = "knight.png";
            level = 7;
            MonsterHealth = 15;
            MaxHealth = 15;
            Attack = 15;
            defense = 10;
            speed = 2;
        }

        /// <summary>
        /// Constructor to create an item based on what is passed in
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
            level = newData.level;
            Attribute = newData.Attribute;
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
                            "Level : " + level;

            return myReturn.Trim();
        }


        ////public string Name { get; set; } = "Michelle";

        ////public string ImageURI { get; set; }
        //// The Descirption of the Item
        //public bool Alive { get; set; } = true;

        ////public List<AttackOption> Attacks { get; set; }

        //public int Level { get; set; } = 1;
        //public int Experience { get; set; } = 0;

        //public int Speed { get; set; } = 1;
        //public int Attack { get; set; } = 1;
        //public int Defense { get; set; } = 1;
        //public int MaxHealth { get; set; } = 1;
        //public int CurrentHealth { get; set; } = 1;

        ////public ItemModel Head { get; set; }
        ////public ItemModel Feet { get; set; }
        ////public ItemModel Necklace { get; set; }
        ////public ItemModel RightFinger { get; set; }
        ////public ItemModel LeftFinger { get; set; }
        //public BaseCharacter(BaseCharacter data)
        //{
        //    this.CharType = data.CharType;
        //    this.Id = data.Id;
        //    this.ImageURI = data.ImageURI;
        //    this.Alive = true; //Copy of character should be Alive no matter what
        //    //this.Attacks = data.Attacks;
        //    this.Level = data.Level;
        //    this.Speed = data.Speed;
        //    this.Attack = data.Attack;
        //    this.Defense = data.Defense;
        //    this.MaxHealth = data.MaxHealth;
        //    this.CurrentHealth = this.MaxHealth; //Should start with max HP
        //    //this.RightFinger = data.RightFinger;
        //    //this.LeftFinger = data.LeftFinger;
        //    //this.Head = data.Head;
        //    //this.Feet = data.Feet;
        //    this.Experience = data.Experience;
        //    //this.Necklace = data.Necklace;
        //}



        //public CharacterTypeEnum CharType { get; set; } = CharacterTypeEnum.Bravery;
        ////public string Id { get; set; } = Guid.NewGuid().ToString();

        //// The Name of the Item 

        //public void Update(BaseCharacter data)
        //{
        //    this.CharType = data.CharType;
        //    this.Id = data.Id;
        //    this.ImageURI = data.ImageURI;
        //    this.Alive = data.Alive; //Copy of character should be Alive no matter what
        //    //this.Attacks = data.Attacks;
        //    this.Level = data.Level;
        //    this.Speed = data.Speed;
        //    this.Attack = data.Attack;
        //    this.Defense = data.Defense;
        //    this.Experience = data.Experience;
        //    this.MaxHealth = data.MaxHealth;
        //    this.CurrentHealth = data.CurrentHealth; //Should start with max HP
        //    //this.RightFinger = data.RightFinger;
        //    //this.LeftFinger = data.LeftFinger;
        //    //this.Head = data.Head;
        //    //this.Feet = data.Feet;
        //    //this.Necklace = data.Necklace;
        //}
        //public string FormatOutput()
        //{
        //    string output = "Name: ";
        //    output += this.Name;
        //    output += " Class: ";
        //    output += this.CharType;
        //    output += " Level: ";
        //    output += this.Level;
        //    return output;
        //}
        //public bool LevelUp()
        //{
        //    Level checkAgainst = new Level();

        //    bool returnMe = false;
        //    if (Experience > checkAgainst.LevelThreshold[Level])
        //    {
        //        Level++;
        //        returnMe = true;
        //    }
        //    checkAgainst = null;
        //    return returnMe;
        //}
        //public bool AddExperience(int exp)
        //{
        //    if(this.Alive)
        //    {
        //        Experience += exp;
        //        return true;
        //    }
        //    return false;
        //}
        //public bool TakeDamage(int dmg)
        //{
        //    bool returnMe = false;
        //    if(this.Alive)
        //    {
        //        CurrentHealth -= dmg;
        //        returnMe = true;
        //    }
        //    if (CurrentHealth <= 0)
        //        Alive = false;
        //    return returnMe;
        //}
        ////public int GetAttack(AttackOption atk)
        ////{
        ////    int attack = atk.AttackBuff;
        ////    //if (LeftFinger.Attribute == AttributeEnum.Attack)
        ////    //    attack += LeftFinger.Value;
        ////    //if (RightFinger.Attribute == AttributeEnum.Attack)
        ////    //    attack += RightFinger.Value;
        ////    //if (Necklace.Attribute == AttributeEnum.Attack)
        ////    //    attack += Necklace.Value;
        ////    //if (Head.Attribute == AttributeEnum.Attack)
        ////    //    attack += Head.Value;
        ////    //if (Feet.Attribute == AttributeEnum.Attack)
        ////    //    attack += Feet.Value;
        ////    return attack;
        ////}

    }
}
