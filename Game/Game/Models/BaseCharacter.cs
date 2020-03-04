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

        /// <summary>
        /// Default character
        /// 
        /// Gets a type, guid, name and description
        /// </summary>
        public BaseCharacter()
        {
            PlayerType = PlayerTypeEnum.Character;
            Attribute = CharacterTypeEnum.Bravery;
            Guid = Id;
            Name = "Michelle";
            Description = "Happy CS Student with a passion for Marine Bio";
            Level = 1;
            ImageURI = "Michelle.png";
            Experience = 0;
            ExperienceRemaining = Helpers.LevelTableHelper.Instance.LevelDetailsList[Level + 1].Experience - 1;
        }

        /// <summary>
        /// Create a copy
        /// </summary>
        /// <param name="data"></param>
        public BaseCharacter(BaseCharacter data)
        {
            Update(data);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public override bool Update(BaseCharacter newData)
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

            // Difficulty = newData.Difficulty;

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
        /// Helper to combine the attributes into a single line, to make it easier to display the item as a string
        /// </summary>
        /// <returns></returns>
        public override string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();
            myReturn += " , Total Experience : " + Experience;
            myReturn += " , Attack :" + GetAttackTotal;
            myReturn += " , Defense :" + GetDefenseTotal;
            myReturn += " , Speed :" + GetSpeedTotal;
            myReturn += " , Items : " + ItemSlotsFormatOutput();
            myReturn += " , Damage : " + GetDamageTotalString;

            return myReturn;
        }

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
