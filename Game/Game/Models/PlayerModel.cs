using Game.Helpers;
using Game.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
namespace Game.Models
{
    public class PlayerModel<T> : BaseModel<T>
    {
        #region Attributes
        #region GameEngineAttributes
        //alive status, !alive will be removed from the list
        [Ignore]

        public bool Alive { get; set; } = true;

        //the type of player, character comes before monster
        [Ignore]
        public PlayerTypeEnum PlayerType{ get; set; } = PlayerTypeEnum.Unknown;

        //TurnOrder
        [Ignore]
        public int Order { get; set; } = 0;

        //Remember who was first into the list ...
        [Ignore]
        public int ListOrder { get; set; } = 0;

        #endregion GameEngineAttributes

        #region PlayerAttributes

        //  character level info for character or monster
        public uint Level { get; set; } = 1;

        // character total experience //
        public uint Experience { get; set; } = 0;

        // Enum of the different "CLass" that the character has

        // characer stats//
        //Current Health
        public uint CurrHealth { get; set; } = 0;

        //Total Expereince
        public int ExpereinceTotal { get; set; } = 0;
        // MaxHealth
        public uint MaxHealth { get; set; } = 1;

        //Used to calculate ToHit roll
        public uint Attack { get; set; } = 0;
        //used to determine ToHit contest
        public uint Defense { get; set; } = 0;
        //Used to determine turn order in battle
        public uint Speed { get; set; } = 0;

        #endregion PlayerAttributes

        #endregion Attributes

        #region Items
        //Items held by a character

        // ItemModel is a string referencing the database table
        public string HeadItem { get; set; } = null;

        //NeckItem referencing the database table
        public string NeckItem { get; set; } = null;

        // Offhand is a string referencing the database table
        public string OffHand { get; set; } = null;

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; } = null;

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; } = null;

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; } = null;

        public bool IsAlive = true;
        ////////////////////////////////////////////////////
        ///
        #endregion Items


        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>







        //Method to remove HP from a character and returns if a Char is dead
        public bool TakeDamage(uint Damage)
        {
            CurrHealth -= Damage;
            if (CurrHealth <= 0)
                IsAlive = false;
            return IsAlive;
        }

        //public int GetAttack() { }
        //Used to determine the Class of a character


        //Used to reduce mana points after a special attack is used

        //Called when some special item is used


    }
}
