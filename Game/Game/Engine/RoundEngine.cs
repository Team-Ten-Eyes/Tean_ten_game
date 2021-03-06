﻿using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Helpers;
using System.Diagnostics;
namespace Game.Engine
{
    /// <summary>
    /// Manages the Rounds
    /// </summary>
    public class RoundEngine : TurnEngine
    {



        public bool testBossHack { get; set; } = false;
        /// <summary>
        /// Clear the List between Rounds
        /// will only clear potions and monster
        /// </summary>
        /// <returns></returns>
        private bool ClearLists()
        {
            MonsterList.Clear();
            potionPool.Clear();

            if (MonsterList.Count == 0 && potionPool.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Call to make a new set of monsters...
        /// </summary>
        /// <returns></returns>
        public bool NewRound()
        {
            // End the existing round
            EndRound();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            //populate the potions for the round
            populatePotionsList();
            

            // Set Order for the Round
            OrderPlayerListByTurnOrder();

            for (int i = 0; i < PlayerList.Count; i++)
            {
                if (PlayerList[i].PlayerType == PlayerTypeEnum.Character && PlayerList[i].Name == "Mike")
                {
                    Debug.WriteLine("Mike Has Died");
                    PlayerList[i].Alive = false;
                }
            }

            // Update Score for the RoundCount
            BattleScore.RoundCount++;
            //Roll for Hack 48 condition
            deathRollHack48 = DiceHelper.RollDice(1, 20);
            
            return true;
        }

        /// <summary>
        /// Add Monsters to the Round
        /// 
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///   
        /*
            * Hint: 
            * I don't have crudi monsters yet so will add 6 new ones...
            * If you have crudi monsters, then pick from the list

            * Consdier how you will scale the monsters up to be appropriate for the characters to fight
            * 
            */
        /// </summary>
        /// <returns></returns>
        public int AddMonstersToRound()
        {
            // TODO: Teams, You need to implement your own Logic can not use mine.




            int TargetLevel = 1;

            if (CharacterList.Count() > 0)
            {
                // Get the Min Character Level (linq is soo cool....)
                TargetLevel = Convert.ToInt32(CharacterList.Min(m => m.Level));
            }

            for (var i = 0; i < MaxNumberPartyMonsters; i++)
            {
                var data = Helpers.RandomPlayerHelper.GetRandomMonster(TargetLevel);

                // Help identify which Monster it is
                data.Name += " " + MonsterList.Count() + 1;

                //Scenario 31
                if (BattleScore.RoundCount >= 100) {
                    data.Attack *= 10;
                    data.Speed *= 10;
                    data.Defense *= 10;
                    data.CurrHealth *= 10;
                    data.MaxHealth *= 10;
                }


                MonsterList.Add(new PlayerInfoModel(data));
            }

            
            if((BossBattleFunctionality && DiceHelper.RollDice(1,100) > 90) || testBossHack)
            {
                Debug.WriteLine("BOSS MONSTER APPROACHING!!!!!");
                MonsterList.Clear();
                int scaleFactor = 0;
                for(int i = 0; i < CharacterList.Count; i++)
                {
                    scaleFactor += CharacterList[i].Level;
                }
                if (scaleFactor > 20)
                    scaleFactor = 20;



                var data = new BaseMonster();
                data.LevelUpToValue(scaleFactor);

                data.Attack = 5000;
                data.Defense = 5000;
                data.Speed = 10000;
                data.MaxHealth = data.CurrHealth = 1000;
                
                MonsterList.Add(new PlayerInfoModel(data));
            }


            return MonsterList.Count();
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public bool EndRound()
        {
            // In Auto Battle this happens and the characters get their items, In manual mode need to do it manualy
            if (BattleScore.AutoBattle)
            {
                PickupItemsForAllCharacters();
            }

            // Reset Monster and Item Lists
            ClearLists();

            return true;
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        public void PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                PickupItemsFromPool(character);
            }
        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        /// <returns></returns>
        public RoundEnum RoundNextTurn()
        {

            // No characters, game is over...
            if (CharacterList.Count < 1)
            {
                // Game Over
                RoundStateEnum = RoundEnum.GameOver;
                return RoundStateEnum;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            // Decide Who gets next turn
            // Remember who just went...
            if (BattleScore.AutoBattle)
            {
                // Decide Who gets next turn
                // Remember who just went...
                CurrentAttacker = GetNextPlayerTurn();

                // Only Attack for now
                //CurrentAction = ActionEnum.Attack;
            }
            // Do the turn....

            TakeTurn(CurrentAttacker);

            RoundStateEnum = RoundEnum.NextTurn;

            return RoundStateEnum;
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerTurn()
        {
            // Remove the Dead
            RemoveDeadPlayersFromList();

            // Get Next Player
            var PlayerCurrent = GetNextPlayerInList();

            return PlayerCurrent;
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        /// <returns></returns>
        public List<PlayerInfoModel> RemoveDeadPlayersFromList()
        {
            PlayerList = PlayerList.Where(m => m.Alive == true).ToList();
            return PlayerList;
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            //Situation 15
            Random rand = new Random();
            if (SpeedAlways == true || (Speed20 == true && rand.Next(1, 6) == 1)) {
                PlayerList = PlayerList.OrderBy(a => a.GetSpeed())
                    .ThenByDescending(a => a.Experience)
                    .ThenByDescending(a => a.Name)
                    .ThenByDescending(a => a.ListOrder)
                    .ToList();
                return PlayerList;

            }

            PlayerList = PlayerList.OrderByDescending(a => a.GetSpeed())
                .ThenByDescending(a => a.Experience)
                .ThenByDescending(a => a.Name)
                .ThenByDescending(a => a.ListOrder)
                .ToList();

            return PlayerList;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public List<PlayerInfoModel> MakePlayerList()
        {
            // Start from a clean list of players
            PlayerList.Clear();

            // Remember the Insert order, used for Sorting
            var ListOrder = 0;

            foreach (var data in CharacterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            foreach (var data in MonsterList)
            {
                if (data.Alive)
                {
                    PlayerList.Add(
                        new PlayerInfoModel(data)
                        {
                            // Remember the order
                            ListOrder = ListOrder
                        });

                    ListOrder++;
                }
            }

            return PlayerList;
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public PlayerInfoModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (PlayerList.Count == 0)
            {
                return null;
            }

            // No current player, so set the first one
            if (CurrentAttacker == null)
            {
                return PlayerList.FirstOrDefault();
            }

            // Find current player in the list
            var index = PlayerList.FindIndex(m => m.Guid.Equals(CurrentAttacker.Guid));

            // If at the end of the list, return the first element
            if (index == PlayerList.Count() - 1)
            {
                return PlayerList.FirstOrDefault();
            }

            // Return the next element
            return PlayerList[index + 1];
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        /// <param name="character"></param>
        public bool PickupItemsFromPool(PlayerInfoModel character)
        {

            
            //if (BattleScore.AutoBattle)
            {
                // Have the character, walk the items in the pool, and decide if any are better than current one.

                GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
                GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
            }
            return true;
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        public bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            var myList = ItemPool.Where(a => a.Location == setLocation)
                .OrderByDescending(a => a.Value)
                .ToList();

            // If no items in the list, return...
            if (!myList.Any())
            {
                return false;
            }

            var CharacterItem = character.GetItemByLocation(setLocation);
            if (CharacterItem == null)
            {
                SwapCharacterItem(character, setLocation, myList.FirstOrDefault());
                return true;
            }

            foreach (var PoolItem in myList)
            {
                if (PoolItem.Value > CharacterItem.Value)
                {
                    SwapCharacterItem(character, setLocation, PoolItem);
                    return true;
                }
            }

            return true;
        }

        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        /// <param name="PoolItem"></param>
        /// <returns></returns>
        public ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem)
        {
            // Put on the new ItemModel, which drops the one back to the pool
            var droppedItem = character.AddItem(setLocation, PoolItem.Id);

            // Add the PoolItem to the list of selected items
            BattleScore.ItemModelSelectList.Add(PoolItem);

            // Remove the ItemModel just put on from the pool
            ItemPool.Remove(PoolItem);

            if (droppedItem != null)
            {
                // Add the dropped ItemModel to the pool
                ItemPool.Add(droppedItem);
            }

            return droppedItem;
        }

        /// <summary>
        /// this is hack number 4 which will give the user 6
        /// this will also add two mana potions to the character as well
        /// </summary>
        /// <returns></returns>
        public bool populatePotionsList()
        {
            if (RoundHealing == RoundHealingEnum.Healing_on)  //if on will add six healing potions
            {
                for (int i = 0; i < 6; i++)
                {
                    //creating a health potion
                    PotionsModel HealthPotion = new PotionsModel();
                    HealthPotion.Addition = (5 * BattleScore.RoundCount);
                    HealthPotion.potionType = PotionsEnum.Health;
                    HealthPotion.ImageURI = "Health.png";
                    //adding to the potionsList
                    potionPool.Add(HealthPotion);
                }
            }
            else if (RoundHealing == RoundHealingEnum.Healing_off)
            {
                PotionsModel HealthPotion = new PotionsModel();
                HealthPotion.Addition = (5 * BattleScore.RoundCount);
                HealthPotion.potionType = PotionsEnum.Health;
                HealthPotion.ImageURI = "Health.png";
                //only adding one potion
                potionPool.Add(HealthPotion);
            }
            for (int i = 0; i < 2; i++) //defualt is that you will always get two mana potions
            {
                //creating a mana potions
                PotionsModel ManaPotion = new PotionsModel();
                ManaPotion.Addition = (5 * BattleScore.RoundCount);
                ManaPotion.potionType = PotionsEnum.Mana;
                ManaPotion.ImageURI = "Mana.png";
                // adding to the potionList
                potionPool.Add(ManaPotion);
            }


            if (potionPool.Count == 8 && RoundHealing == RoundHealingEnum.Healing_on)
                return true;
            else if (RoundHealing == RoundHealingEnum.Healing_on)
                return false;
            else if (RoundHealing == RoundHealingEnum.Healing_off && potionPool.Count == 3)
                return true;

            return false;
        }
    }
}