using Game.Models;
using Game.Views;
using Game.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.ComponentModel;
using Game.Services;
using System.Collections.Generic;


namespace Game.Engine
{
    /// <summary>
    /// Battle Engine for the Game
    /// </summary>
    public class BattleEngine : RoundEngine
    {
        // Track if the Battle is Running or Not
        public bool BattleRunning = false;

        /// <summary>
        /// Add the charcter to the character list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 


        public async Task<bool> RunBattle()
        {
            RoundEnum RoundCondition;

            // Start Battle
            StartBattle(false);

            // Fight Loop. Continue until Game is Over...
            do
            {
                // Check for excessive duration.
                

                Debug.WriteLine("Next Turn");

                // Do the turn...
                // If the round is over start a new one...
                RoundCondition = RoundNextTurn();

                if (RoundCondition == RoundEnum.NewRound)
                {
                    NewRound();
                    await App.Current.MainPage.Navigation.PushModalAsync(new NewRoundPage());
                    Debug.WriteLine("New Round");
                }

            } while (RoundCondition != RoundEnum.GameOver);

            Debug.WriteLine("Game Over");
            //push game over page with score model


            // Wrap up
            EndBattle();
            await App.Current.MainPage.Navigation.PushModalAsync(new ScorePage(new GenericViewModel<ScoreModel>(BattleScore)));

            return true;
        }


        public bool PopulateCharacterList(BaseCharacter data)
        {
            CharacterList.Add(new PlayerInfoModel(data));

            return true;
        }


        /// <summary>
        /// Start the Battle
        /// </summary>
        /// <param name="isAutoBattle"></param>
        /// <returns></returns>
        public bool StartBattle(bool isAutoBattle)
        {
            // Reset the Score so it is fresh
            BattleScore = new ScoreModel
            {
                AutoBattle = isAutoBattle
            };

            BattleRunning = true;

            fillItemPool();
        
            NewRound();
            
            

            return true;
        }


        /// <summary>
        /// will fill the item pool with default
        /// </summary>
        /// <returns></returns>
        public bool fillItemPool()
        {
            ItemModel temp = new ItemModel();
            List <ItemModel> item = Services.DefaultData.LoadData(temp);
            foreach (ItemModel i in item)
            {
                ItemPool.Add(i);
            }
            return true;
        }

        
        


        /// <summary>
        /// End the Battle
        /// </summary>
        /// <returns></returns>
        public bool EndBattle()
        {
            BattleRunning = false;

            BattleScore.ScoreTotal = BattleScore.CalculateScore();

            return true;
        }


        ///// <summary>
        ///// this is hack number 4 which will give the user 6
        ///// this will also add two mana potions to the character as well
        ///// </summary>
        ///// <returns></returns>
        //public bool populatePotionsList()
        //{
        //    if (RoundHealing == RoundHealingEnum.Healing_on)  //if on will add six healing potions
        //    {
        //        for (int i = 0; i < 6; i++)
        //        {
        //            //creating a health potion
        //            PotionsModel HealthPotion = new PotionsModel();
        //            HealthPotion.Addition = (5 * BattleScore.RoundCount);
        //            HealthPotion.potionType = PotionsEnum.Health;
        //            HealthPotion.ImageURI = "Health.png";
        //            //adding to the potionsList
        //            potionPool.Add(HealthPotion);
        //        }
        //    }
        //    else if(RoundHealing == RoundHealingEnum.Healing_off)
        //    {
        //        PotionsModel HealthPotion = new PotionsModel();
        //        HealthPotion.Addition = (5 * BattleScore.RoundCount);
        //        HealthPotion.potionType = PotionsEnum.Health;
        //        HealthPotion.ImageURI = "Health.png";
        //        //only adding one potion
        //        potionPool.Add(HealthPotion);
        //    }
        //    for (int i = 0; i < 2; i++) //defualt is that you will always get two mana potions
        //        {
        //            //creating a mana potions
        //            PotionsModel ManaPotion = new PotionsModel();
        //            ManaPotion.Addition = (5 * BattleScore.RoundCount);
        //            ManaPotion.potionType = PotionsEnum.Mana;
        //            ManaPotion.ImageURI = "Mana.png";
        //            // adding to the potionList
        //            potionPool.Add(ManaPotion);
        //    }


        //    if (potionPool.Count == 8 && RoundHealing == RoundHealingEnum.Healing_on)
        //        return true;
        //    else if (RoundHealing == RoundHealingEnum.Healing_on)
        //        return false;
        //    else if (RoundHealing == RoundHealingEnum.Healing_off && potionPool.Count == 3)
        //        return true;

        //    return false;
        //}
    }
}