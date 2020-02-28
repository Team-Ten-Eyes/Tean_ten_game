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
        public PlayerTypeEnum PlayerType { get; set; } = PlayerTypeEnum.Unknown;

        //TurnOrder
        [Ignore]
        public int Order { get; set; } = 0;

        //Remember who was first into the list ...
        [Ignore]
        public int ListOrder { get; set; } = 0;

        #endregion GameEngineAttributes

        #region PlayerAttributes

        //  character level info for character or monster
        public int Level { get; set; } = 1;
        public DifficultyEnum Difficulty { get; set; } = DifficultyEnum.Unknown;
        public int ExperienceRemaining { get; set; }
        // character total experience //
        public int Experience { get; set; } = 0;

        // Enum of the different "CLass" that the character has

        // characer stats//
        //Current Health
        public int CurrHealth { get; set; } = 0;

        //Total Expereince
        public int ExpereinceTotal { get; set; } = 0;
        // MaxHealth
        public int MaxHealth { get; set; } = 1;

        //Used to calculate ToHit roll
        public int Attack { get; set; } = 0;
        //used to determine ToHit contest
        public int Defense { get; set; } = 0;
        //Used to determine turn order in battle
        public int Speed { get; set; } = 0;

        #endregion PlayerAttributes

        #endregion Attributes

        #region Items
        // ItemModel is a string referencing the database table
        public string Head { get; set; } = null;

        // Feet is a string referencing the database table
        public string Feet { get; set; } = null;

        // Necklasss is a string referencing the database table
        public string Necklass { get; set; } = null;

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; } = null;

        // Offhand is a string referencing the database table
        public string OffHand { get; set; } = null;

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; } = null;

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; } = null;
        #endregion Items

        #region AttributeDisplay

        // Following returns the values for each of the attributes with the modifiers

        #region Attack        
        [Ignore]
        // Return the attack value
        public int GetAttackLevelBonus { get { return LevelTableHelper.Instance.LevelDetailsList[Level].Attack; } }

        [Ignore]
        // Return the Attack with Item Bonus
        public int GetAttackItemBonus { get { return GetItemBonus(AttributeEnum.Attack); } }

        [Ignore]
        // Return the Total of All Attack
        public int GetAttackTotal { get { return GetAttack(); } }
        #endregion Attack

        #region Defense
        [Ignore]
        // Return the Defense value
        public int GetDefenseLevelBonus { get { return LevelTableHelper.Instance.LevelDetailsList[Level].Defense; } }

        [Ignore]
        // Return the Defense with Item Bonus
        public int GetDefenseItemBonus { get { return GetItemBonus(AttributeEnum.Defense); } }

        [Ignore]
        // Return the Total of All Defense
        public int GetDefenseTotal { get { return GetDefense(); } }
        #endregion Defense

        #region Speed
        [Ignore]
        // Return the Speed value
        public int GetSpeedLevelBonus { get { return LevelTableHelper.Instance.LevelDetailsList[Level].Speed; } }

        [Ignore]
        // Return the Speed with Item Bonus
        public int GetSpeedItemBonus { get { return GetItemBonus(AttributeEnum.Speed); } }

        [Ignore]
        // Return the Total of All Speed
        public int GetSpeedTotal { get { return GetSpeed(); } }
        #endregion Speed

        #region CurrentHealth
        [Ignore]
        // Return the CurrentHealth value
        public int GetCurrentHealthLevelBonus { get { return 0; } }

        [Ignore]
        // Return the CurrentHealth with Item Bonus
        public int GetCurrentHealthItemBonus { get { return GetItemBonus(AttributeEnum.CurrentHealth); } }

        [Ignore]
        // Return the Total of All CurrentHealth
        public int GetCurrentHealthTotal { get { return GetCurrentHealth(); } }
        #endregion CurrentHealth

        #region MaxHealth
        [Ignore]
        // Return the MaxHealth value
        public int GetMaxHealthLevelBonus { get { return 0; } }

        [Ignore]
        // Return the MaxHealth with Item Bonus
        public int GetMaxHealthItemBonus { get { return GetItemBonus(AttributeEnum.MaxHealth); } }

        [Ignore]
        // Return the Total of All MaxHealth
        public int GetMaxHealthTotal { get { return GetMaxHealth(); } }
        #endregion MaxHealth

        #region Damage
        [Ignore]
        // Return the Damage value, it is 25% of the Level rounded up
        public int GetDamageLevelBonus { get { return Convert.ToInt32(Math.Ceiling(Level * .25)); } }

        [Ignore]
        // Return the Damage with Item Bonus
        public int GetDamageItemBonus
        {
            get
            {
                var myItem = ItemIndexViewModel.Instance.GetItem(PrimaryHand);
                if (myItem == null)
                {
                    return 0;
                }
                return myItem.Damage;
            }
        }

        [Ignore]
        // Return the Damage Dice if there is one
        public string GetDamageItemBonusString
        {
            get
            {
                var data = GetDamageItemBonus;
                if (data == 0)
                {
                    return "-";
                }

                return string.Format("1D {0}", data);
            }
        }

        [Ignore]
        // Return the Total of All Damage
        public string GetDamageTotalString
        {
            get
            {

                if (GetDamageItemBonusString.Equals("-"))
                {
                    return GetDamageLevelBonus.ToString();
                }

                return GetDamageLevelBonus.ToString() + " + " + GetDamageItemBonusString;

            }
        }
        #endregion Damage

        #endregion AttributeDisplay

        
        #region Methods

        public PlayerModel()
        {
            Guid = Id;
        }

        #region GetAttributeValues
        /// <summary>
        /// Return the Total Attack Value
        /// </summary>
        /// <returns></returns>
        virtual public int GetAttack()
        {
            // Base Attack
            var myReturn = Attack;

            // Attack Bonus from Level
            myReturn += GetAttackLevelBonus;

            // Get Attack bonus from Items
            myReturn += GetAttackItemBonus;

            return myReturn;
        }

        /// <summary>
        /// Return the Total Defense Value
        /// </summary>
        /// <returns></returns>
        public int GetDefense()
        {
            // Base Defense
            var myReturn = Defense;

            // Defense Bonus from Level
            myReturn += GetDefenseLevelBonus;

            // Get Defense bonus from Items
            myReturn +=  GetDefenseItemBonus;

            return myReturn;
        }

        /// <summary>
        /// Return the Total Speed Value
        /// </summary>
        /// <returns></returns>
        public int GetSpeed()
        {
            // Base Speed
            var myReturn = Speed;

            // Speed Bonus from Level
            myReturn += GetSpeedLevelBonus;

            // Get Speed bonus from Items
            myReturn += GetSpeedItemBonus;

            return myReturn;
        }

        /// <summary>
        /// Return the Total CurrentHealth Value
        /// </summary>
        /// <returns></returns>
        public int GetCurrentHealth()
        {
            // Base CurrentHealth
            var myReturn = CurrHealth;

            // CurrentHealth Bonus from Level
            myReturn += GetCurrentHealthLevelBonus;

            // Get CurrentHealth bonus from Items
            myReturn +=  GetCurrentHealthItemBonus;

            return myReturn;
        }

        /// <summary>
        /// Return the Total MaxHealth Value
        /// </summary>
        /// <returns></returns>
        public int GetMaxHealth()
        {
            // Base MaxHealth
            var myReturn = MaxHealth;

            // MaxHealth Bonus from Level
            myReturn += GetMaxHealthLevelBonus;

            // Get MaxHealth bonus from Items
            myReturn +=  GetMaxHealthItemBonus;

            return myReturn;
        }
        #endregion GetAttributeValues

        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public bool TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                return false;
            }

            CurrHealth = CurrHealth - damage;
            if (CurrHealth <= 0)
            {
                CurrHealth = 0;

                // Death...
                CauseDeath();
            }

            return true;
        }

        public int LevelUpToValue(int Value)
        {
            // Adjust the experience to the min for that level.
            // That will trigger level up to happen

            if (Value < 0)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value <= Level)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value > LevelTableHelper.MaxLevel)
            {
                return Level;
            }

            AddExperience(LevelTableHelper.Instance.LevelDetailsList[Value].Experience + 1);

            return Level;
        }



        /// <summary>
        /// Roll the Damage Dice, and add to the Damage
        /// </summary>
        /// <returns></returns>
        virtual public int GetDamageRollValue()
        {


            var myReturn = 0;

            var myItem = ItemIndexViewModel.Instance.GetItem(PrimaryHand);
            if (myItem != null)
            {
                // Dice of the weapon.  So sword of Damage 10 is d10
                myReturn += DiceHelper.RollDice(1, myItem.Damage);
            }

            // Add in the Level as extra damage per game rules
            myReturn += GetDamageLevelBonus;

            return myReturn;
        }

        // Death
        // Alive turns to False
        public bool CauseDeath()
        {
            Alive = false;
            return Alive;
        }

        public string FormatOutput() { return ""; }

        public bool AddExperience(int newExperience) { return true; }

        public int CalculateExperienceEarned(int damage) { return 0; }

        #region Items
        // Get the Item at a known string location (head, foot etc.)
        public ItemModel GetItem(string itemString)
        {
            return ItemIndexViewModel.Instance.GetItem(itemString);
        }

        // Drop All Items
        // Return a list of items for the pool of items
        public List<ItemModel> DropAllItems()
        {
            var myReturn = new List<ItemModel>();

            // Drop all Items
            ItemModel myItem;

            myItem = RemoveItem(ItemLocationEnum.Head);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Necklass);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.PrimaryHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.OffHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.RightFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.LeftFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Feet);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            return myReturn;
        }

        // Remove ItemModel from a set location
        // Does this by adding a new ItemModel of Null to the location
        // This will return the previous ItemModel, and put null in its place
        // Returns the ItemModel that was at the location
        // Nulls out the location
        public ItemModel RemoveItem(ItemLocationEnum itemlocation)
        {
            var myReturn = AddItem(itemlocation, null);

            // Save Changes
            return myReturn;
        }

        // Get the ItemModel at a known string location (head, foot etc.)
        public ItemModel GetItemByLocation(ItemLocationEnum itemLocation)
        {
            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    return GetItem(Head);

                case ItemLocationEnum.Necklass:
                    return GetItem(Necklass);

                case ItemLocationEnum.PrimaryHand:
                    return GetItem(PrimaryHand);

                case ItemLocationEnum.OffHand:
                    return GetItem(OffHand);

                case ItemLocationEnum.RightFinger:
                    return GetItem(RightFinger);

                case ItemLocationEnum.LeftFinger:
                    return GetItem(LeftFinger);

                case ItemLocationEnum.Feet:
                    return GetItem(Feet);
            }

            return null;
        }

        // Add ItemModel
        // Looks up the ItemModel
        // Puts the ItemModel ID as a string in the location slot
        // If ItemModel is null, then puts null in the slot
        // Returns the ItemModel that was in the location
        public ItemModel AddItem(ItemLocationEnum itemlocation, string itemID)
        {
            ItemModel myReturn;

            switch (itemlocation)
            {
                case ItemLocationEnum.Feet:
                    myReturn = GetItem(Feet);
                    Feet = itemID;
                    break;

                case ItemLocationEnum.Head:
                    myReturn = GetItem(Head);
                    Head = itemID;
                    break;

                case ItemLocationEnum.Necklass:
                    myReturn = GetItem(Necklass);
                    Necklass = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    myReturn = GetItem(PrimaryHand);
                    PrimaryHand = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    myReturn = GetItem(OffHand);
                    OffHand = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    myReturn = GetItem(RightFinger);
                    RightFinger = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    myReturn = GetItem(LeftFinger);
                    LeftFinger = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }

            return myReturn;
        }

        // Walk all the Items on the Character.
        // Add together all Items that modify the Attribute Enum Passed in
        // Return the sum
        public int GetItemBonus(AttributeEnum attributeEnum)
        {
            var myReturn = 0;
            ItemModel myItem;

            myItem = GetItem(Head);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                   myReturn += myItem.Value;
                }
            }

            myItem = GetItem(Necklass);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(PrimaryHand);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(OffHand);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(RightFinger);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(LeftFinger);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(Feet);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            return myReturn;
        }

        #endregion Items

        #endregion Methods
    }
}