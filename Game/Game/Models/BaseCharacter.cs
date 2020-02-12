 using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class BaseCharacter :BaseModel<BaseCharacter>
    {


        // Range of the item, swords are 1, hats/rings are 0, bows are >1
        public int level { get; set; } = 1;

        // The Damage the Item can do if it is used as a weapon in the primary hand
        public int Experience { get; set; } = 0;

        // Enum of the different attributes that the character modifies, Items can only modify one character
        public CharacterTypeEnum Attribute { get; set; } = CharacterTypeEnum.Bravery; //defaults to bravery

        // Where the Item goes on the character.  Head, Foot etc.
        public ItemLocationEnum Location { get; set; } = ItemLocationEnum.Unknown;

        // characer stats/////////////////////
        public int Attack { get; set; } = 0;

        public int defense { get; set; } = 0;

        public int speed { get; set; } = 0;


        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public BaseCharacter()
        {
            ImageURI = "knight.png";
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
            Description = newData.Description;
            Attack = newData.Attack;
            Attribute = newData.Attribute;
            Location = newData.Location;
            Name = newData.Name;
            Description = newData.Description;
            ImageURI = newData.ImageURI;
            level = newData.level;
            Experience= newData.Experience;
            defense = newData.defense;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description + " for " +
                            Location.ToString() + " with " +
                            Attribute.ToString() +
                            "+" + Attack + " , " +
                            "Experience : " + Experience + " , " +
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
