using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Game.Engine
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     * 
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : BaseEngine
    {
        ///HACKATHON STUFF
        public int deathRollHack48 {get;set;}
        public bool testHack48 { get; set; } = false;

        #region Algrorithm
        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            var result = Attack(Attacker);

            BattleScore.TurnCount++;

            return result;
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Attack(PlayerInfoModel Attacker)
        {
            // INFO: Teams, AttackChoice will auto pick the target, good for auto battle
            if (BattleScore.AutoBattle)
            {
                if (Attacker.PlayerType == PlayerTypeEnum.Character && RoundHealing == RoundHealingEnum.Healing_on)
                {
                    foreach (PlayerInfoModel character in CharacterList)
                    {

                        if (bellowTwentyHealth(character))
                        {
                            Debug.WriteLine("OH NO You have a greedy character that drank all your potions");
                            DrinkAllPotions(character);
                            break;
                        }
                    }
                }
                // For Attack, Choose Who
                CurrentDefender = AttackChoice(Attacker);

                if (CurrentDefender == null)
                {
                    return false;
                }
            }
            



                // Do Attack
                TurnAsAttack(Attacker, CurrentDefender);

            return true;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            switch (data.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToAttack();

                case PlayerTypeEnum.Character:
                default:
                    return SelectMonsterToAttack();
            }
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectCharacterToAttack()
        {
            if (PlayerList == null)
            {
                return null;
            }

            if (PlayerList.Count < 1)
            {
                return null;
            }

            // Select first in the list

            // TODO: Teams, You need to implement your own Logic can not use mine.
            var Defender = PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                .OrderBy(m => m.ListOrder).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectMonsterToAttack()
        {
            if (PlayerList == null)
            {
                return null;
            }

            if (PlayerList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 

            // TODO: Teams, You need to implement your own Logic can not use mine.

            var Defender = PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderBy(m => m.CurrHealth).FirstOrDefault();



            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            BattleMessagesModel.ClearMessages();

            // Do the Attack
            CalculateAttackStatus(Attacker, Target);

            if(Attacker.PlayerType == PlayerTypeEnum.Character && Attacker.AttributesPrime())
            {
                BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            }

            // Hackathon
            // Hackathon Scenario 2, Bob alwasys misses
            if (Attacker.Name.Equals("Bob"))
            {
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.TurnMessage = "Bob always Misses";
                Debug.WriteLine(BattleMessagesModel.TurnMessage);
                return true;
            }

            switch (BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss
                    RemoveIfDead(Attacker);
                    break;

                case HitStatusEnum.Hit:
                    // It's a Hit

                    //Calculate Damage
                    BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    if (Attacker.PlayerType == PlayerTypeEnum.Character && Attacker.AttributesPrime())
                    {
                        BattleMessagesModel.DamageAmount = Attacker.GetDamageLevelBonus;
                        var myItem = ItemIndexViewModel.Instance.GetItem(Attacker.PrimaryHand);
                        if (myItem != null)
                        {
                            // Dice of the weapon.  So sword of Damage 10 is d10
                            BattleMessagesModel.DamageAmount = myItem.Damage;
                        }
                        Debug.WriteLine("Prime Damage Done Boyyyy");
                    }

                    // Apply the Damage
                    ApplyDamage(Target);

                    BattleMessagesModel.TurnMessageSpecial = BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    RemoveIfDead(Target);
                    

                    // If it is a character apply the experience earned
                    CalculateExperience(Attacker, Target);

                    break;
            }

            BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.TurnMessageSpecial + BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        /// <param name="Target"></param>
        private void ApplyDamage(PlayerInfoModel Target)
        {
            Target.TakeDamage(BattleMessagesModel.DamageAmount);
            BattleMessagesModel.CurrentHealth = Target.CurrHealth;
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        /// <returns></returns>
        public HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            // Remember Current Player
            BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            // Choose who to attack
            BattleMessagesModel.TargetName = Target.Name;
            BattleMessagesModel.AttackerName = Attacker.Name;

            // Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;

            BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        public bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var experienceEarned = Target.CalculateExperienceEarned(BattleMessagesModel.DamageAmount);
                BattleMessagesModel.ExperienceEarned = " Earned " + experienceEarned + " points";

                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    BattleMessagesModel.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetMaxHealthTotal;
                    Debug.WriteLine(BattleMessagesModel.LevelUpMessage);
                }

                // Add Experinece to the Score
                BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            return true;
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(PlayerInfoModel Target)
        {
            // Check for alive
            if (Target.Alive == false)
            {
                TargetDied(Target);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        /// <param name="Target"></param>
        public bool TargetDied(PlayerInfoModel Target)
        {
            bool found;

            // Mark Status in output
            BattleMessagesModel.TurnMessageSpecial = " and causes death. ";

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the Character to the killed list
                    BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    BattleScore.CharacterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = CharacterList.Remove(CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    BattleScore.MonsterModelDeathList.Add(Target);

                    DropItems(Target);

                    found = MonsterList.Remove(MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = PlayerList.Remove(PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            BattleMessagesModel.TurnMessageSpecial += DroppedMessage;

            BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (testHack48 == true)
            {
                for (int i = 0; i < PlayerList.Count; i++)
                {
                    if (PlayerList[i].PlayerType == PlayerTypeEnum.Character && PlayerList[i].Guid == CurrentAttacker.Guid)
                    {
                        d20 = 2;
                        deathRollHack48 = 2;
                        break;
                    }

                }
            }
            
            if(d20 == deathRollHack48 && CurrentAttacker.PlayerType==PlayerTypeEnum.Character)
            {
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                for(int i = 0; i < PlayerList.Count; i ++)
                {
                    if(PlayerList[i].Guid==CurrentAttacker.Guid)
                    {
                        Debug.WriteLine("The CIA regrets to inform you that your character died.");
                        PlayerList[i].Alive = false;
                        BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                        return BattleMessagesModel.HitStatus;
                    }
                }
            }


            if (d20 == 1)
            {
                BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";

                // Force Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                return BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";

                // Force Hit
                BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
                return BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.DamageAmount = 0;
                return BattleMessagesModel.HitStatus;
            }

            BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // TODO: Teams, You need to implement your own modification to the Logic cannot use mine as is.

            // You decide how to drop monster items, level, etc.

            // The Number drop can be Up to the Round Count, but may be less.  
            // Negative results in nothing dropped
            var NumberToDrop = (DiceHelper.RollDice(1, round + 1) - 1);

            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                // Get a random Unique Item
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                result.Add(data);
            }

            return result;
        }

        /// <summary>
        /// Will have the character drink all Health Potions
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool DrinkAllPotions(PlayerInfoModel character)
        {
            
            foreach (PotionsModel potion in potionPool)
            {
                if (potion.GetPotionType() == PotionsEnum.Health)
                {
                    character.addHealth(potion.Addition);
                }
            }
            potionPool = potionPool.Where(x => x.GetPotionType()!= PotionsEnum.Health).ToList();
            return true;
        }


        /// <summary>
        /// will return true if a character is bellow 20 percent health
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool bellowTwentyHealth(PlayerInfoModel character)
        {
            double twenty_percent_mark = (double)(character.MaxHealth * .20);
            if (character.CurrHealth < twenty_percent_mark)
                return true;
            return false;
        }
    }
}