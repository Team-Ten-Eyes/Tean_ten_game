 using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    class BaseCharacter
    {
        public BaseCharacter()
        {
        }
        public BaseCharacter(BaseCharacter data)
        {
            this.CharType = data.CharType;
            this.Id = data.Id;
            this.ImageURI = data.ImageURI;
            this.Alive = true; //Copy of character should be Alive no matter what
            this.Attacks = data.Attacks;
            this.Level = data.Level;
            this.Speed = data.Speed;
            this.Attack = data.Attack;
            this.Defense = data.Defense;
            this.MaxHealth = data.MaxHealth;
            this.CurrentHealth = this.MaxHealth; //Should start with max HP
            this.RightFigner = data.RightFigner;
            this.LeftFinger = data.LeftFinger;
            this.Head = data.Head;
            this.Feet = data.Feet;
            this.Experience = data.Experience;
            this.Necklace = data.Necklace;
        }

        public CharacterTypeEnum CharType { get; set; } = CharacterTypeEnum.Bravery;
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // The Name of the Item 
        public string Name { get; set; } = "This is an Item";

        public string ImageURI { get; set; }
        // The Descirption of the Item
        public bool Alive { get; set; } = true;

        //public List<AttackOption> Attacks { get; set; }

        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;

        public int Speed { get; set; } = 1;
        public int Attack { get; set; } = 1;
        public int Defense { get; set; } = 1;
        public int MaxHealth { get; set; } = 1;
        public int CurrentHealth { get; set; } = 1;

        public ItemModel Head { get; set; }
        public ItemModel Feet { get; set; }
        public ItemModel Necklace { get; set; }
        public ItemModel RightFigner { get; set; }
        public ItemModel LeftFinger { get; set; }
        public void Update(BaseCharacter data)
        {
            this.CharType = data.CharType;
            this.Id = data.Id;
            this.ImageURI = data.ImageURI;
            this.Alive = data.Alive; //Copy of character should be Alive no matter what
            this.Attacks = data.Attacks;
            this.Level = data.Level;
            this.Speed = data.Speed;
            this.Attack = data.Attack;
            this.Defense = data.Defense;
            this.Experience = data.Experience;
            this.MaxHealth = data.MaxHealth;
            this.CurrentHealth = data.CurrentHealth; //Should start with max HP
            this.RightFigner = data.RightFigner;
            this.LeftFinger = data.LeftFinger;
            this.Head = data.Head;
            this.Feet = data.Feet;
            this.Necklace = data.Necklace;
        }
        public string FormatOutput()
        {
            string output = "Name: ";
            output += this.Name;
            output += " Class: ";
            output += this.CharType;
            output += " Level: ";
            output += this.Level;
            return output;
        }
        public bool LevelUp()
        {
            Level checkAgainst = new Level();
            bool returnMe = false;
            if (Experience > checkAgainst.LevelThreshold[Level])
            {
                Level++;
                returnMe = true;
            }
            checkAgainst = null;
            return returnMe;
        }
        
    }
}
