
using Game.Models;

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

        /// <summary>
        /// adding items t0 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool populate_item_list(ItemModel data)
        {
            ItemPool.Add(new ItemModel(data));
            return true;
        }


    }
}