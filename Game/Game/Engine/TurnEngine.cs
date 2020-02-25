using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Helpers;

namespace Game.Engine
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// </summary>
    public class TurnEngine : BaseEngine
    {
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
            // For Attack, Choose Who
            var Target = AttackChoice(Attacker);

            if (Target == null)
            {
                return false;
            }

            // Do Attack
            TurnAsAttack(Attacker, Target);

            CurrentAttacker = new PlayerInfoModel(Attacker);
            CurrentDefender = new PlayerInfoModel(Target);

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
            if (CharacterList == null)
            {
                return null;
            }

            if (CharacterList.Count < 1)
            {
                return null;
            }

            // Select first in the list
            var Defender = CharacterList
                .Where(m => m.Alive)
                .OrderBy(m => m.ListOrder).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel SelectMonsterToAttack()
        {
            if (MonsterList == null)
            {
                return null;
            }

            if (MonsterList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 
            var Defender = MonsterList
                .Where(m => m.Alive)
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

            BattleMessagesModel.TurnMessage = string.Empty;
            BattleMessagesModel.TurnMessageSpecial = string.Empty;
            BattleMessagesModel.AttackStatus = string.Empty;

            BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;


            
                if (Attacker.PlayerType == PlayerTypeEnum.Monster)
                {

                    AttackScore = 20;

                }
                if (Attacker.PlayerType == PlayerTypeEnum.Character)
                {

                    AttackScore = 1;

                }
            


            // Choose who to attack

            BattleMessagesModel.TargetName = Target.Name;
            BattleMessagesModel.AttackerName = Attacker.Name;

            BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            Debug.WriteLine(BattleMessagesModel.GetTurnMessage());

            // It's a Miss
            if (BattleMessagesModel.HitStatus == HitStatusEnum.Miss)
            {
                return true;
            }

            // It's a Hit
            if (BattleMessagesModel.HitStatus == HitStatusEnum.Hit)
            {
                //Calculate Damage
                BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();


               
                    if (Attacker.PlayerType == PlayerTypeEnum.Monster)
                    {

                        BattleMessagesModel.DamageAmount = 20;

                    }
                    if (Attacker.PlayerType == PlayerTypeEnum.Character)
                    {

                        BattleMessagesModel.DamageAmount = 0;

                    }
                

                Target.TakeDamage(BattleMessagesModel.DamageAmount);
            }

            BattleMessagesModel.CurrentHealth = Target.CurrHealth;
            BattleMessagesModel.TurnMessageSpecial = BattleMessagesModel.GetCurrentHealthMessage();

            RemoveIfDead(Target);

            BattleMessagesModel.TurnMessage = Attacker.Name + BattleMessagesModel.AttackStatus + Target.Name + BattleMessagesModel.TurnMessageSpecial;
            Debug.WriteLine(BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// If Dead process Targed Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(PlayerInfoModel Target)
        {
            // Check for alive
            if (Target.Alive == false)
            {
                TargedDied(Target);
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
        public bool TargedDied(PlayerInfoModel Target)
        {
            // Mark Status in output
            BattleMessagesModel.TurnMessageSpecial = " and causes death";

            // Remove target from list...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    CharacterList.Remove(Target);

                    // Add the MonsterModel to the killed list
                    BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    DropItems(Target);

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    MonsterList.Remove(Target);

                    // Add one to the monsters killed count...
                    BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    DropItems(Target);

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(PlayerInfoModel Target)
        {
            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                BattleMessagesModel.TurnMessageSpecial += " ItemModel " + ItemModel.Name + " dropped";
            }

            ItemPool.AddRange(myItemList);

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

            if (d20 == 1)
            {
                // Force Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                return BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                // Force Hit
                BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
                return BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                BattleMessagesModel.AttackStatus = " misses ";
                // Miss
                BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                BattleMessagesModel.DamageAmount = 0;
                return BattleMessagesModel.HitStatus;
            }

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
            // You decide how to drop monster items, level, etc.

            var NumberToDrop = DiceHelper.RollDice(1, round);

            var myList = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                myList.Add(new ItemModel());
            }
            return myList;
        }
    }
}