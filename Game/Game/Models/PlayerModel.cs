using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class PlayerModel<T> : BaseModel<T>
    {
        //  character level info//
        public uint Level { get; set; } = 1;

        // character total experience //
        public uint Experience { get; set; } = 0;

        // Enum of the different "CLass" that the character has

        // characer stats//
        //Current Health
        public uint CurrHealth { get; set; } = 0;

        // MaxHealth
        public uint MaxHealth { get; set; } = 1;

        //Used to calculate ToHit roll
        public uint Attack { get; set; } = 0;
        //used to determine ToHit contest
        public uint Defense { get; set; } = 0;
        //Used to determine turn order in battle
        public uint Speed { get; set; } = 0;


        //Items held by a character

        public string HeadItem { get; set; } = null;
        public string NeckItem { get; set; } = null;
        public string OffHand { get; set; } = null;
        public string PrimaryHand { get; set; } = null;
        public string RightFinger { get; set; } = null;

        public bool IsAlive = true;
        ////////////////////////////////////////////////////

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
