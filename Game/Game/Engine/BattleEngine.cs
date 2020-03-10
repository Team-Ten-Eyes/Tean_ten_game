﻿using Game.Models;
using Game.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
                    //push new round page
                    NewRound();
                    Debug.WriteLine("New Round");
                }

            } while (RoundCondition != RoundEnum.GameOver);

            Debug.WriteLine("Game Over");
            //push game over page with score model


            // Wrap up
            EndBattle();

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

        
            NewRound();
            
                //DO something

            return true;
        }


        
        


        /// <summary>
        /// End the Battle
        /// </summary>
        /// <returns></returns>
        public bool EndBattle()
        {
            BattleRunning = false;

            BattleScore.CalculateScore();

            return true;
        }
    }
}