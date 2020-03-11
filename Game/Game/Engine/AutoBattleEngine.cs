using Game.Models;
using Game.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Engine
{
    /// <summary>
    /// AutoBattle Engine
    /// 
    /// Runs the engine simulation with no user interaction
    /// 
    /// </summary>
    public class AutoBattleEngine : BattleEngine
    {
        #region Algrorithm
        // Prepare for Battle
        // Pick 6 Characters
        // Initialize the Battle
        // Start a Round

        // Fight Loop
        // Loop in the round each turn
        // If Round is over, Start New Round
        // Check end state of round/game

        // Wrap up
        // Get Score
        // Save Score
        // Output Score
        #endregion Algrorithm
        public bool testingHack47 { get; set; }
        // The Battle Engine
        // BattleEngine Engine = new BattleEngine();

        /// <summary>
        /// Return the Score Object
        /// </summary>
        /// <returns></returns>
        public ScoreModel GetScoreObject() { return BattleScore; }

        /// <summary>
        /// Run Auto Battle
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RunAutoBattle()
        {
            RoundEnum RoundCondition;

            Debug.WriteLine("Auto Battle Starting");

            // Auto Battle, does all the steps that a human would do.

            // Prepare for Battle
            if (!testingHack47)
            {
                CreateCharacterParty();
            }
            // Start Battle in AutoBattle mode
            StartBattle(true);

            // Fight Loop. Continue until Game is Over...
            do
            {
                // Check for excessive duration.
                if (DetectInfinateLoop())
                {
                    Debug.WriteLine("Aborting, More than Max Rounds");
                    EndBattle();
                    return false;
                }

                Debug.WriteLine("Next Turn");

                // Do the turn...
                // If the round is over start a new one...
                RoundCondition = RoundNextTurn();

                if (RoundCondition == RoundEnum.NewRound)
                {
                    NewRound();
                    Debug.WriteLine("New Round");
                }

            } while (RoundCondition != RoundEnum.GameOver);

            Debug.WriteLine("Game Over");

            // Wrap up
            EndBattle();

            return true;
        }

        /// <summary>
        /// Check if the Engine is not ending
        /// 
        /// Too many Rounds
        /// Too many Turns in a round
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DetectInfinateLoop()
        {
            if (BattleScore.RoundCount > MaxRoundCount)
            {
                return true;
            }

            if (BattleScore.TurnCount > MaxTurnCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Create Characters for Party
        /// </summary>
        public bool CreateCharacterParty()
        {
            // Picks 6 Characters

            // To use your own characters, populate the List before calling RunAutoBattle

            // Will first pull from existing characters
            foreach (var data in CharacterViewModel.Instance.Dataset)
            {
                if (CharacterList.Count() >= MaxNumberPartyCharacters)
                {
                    break;
                }
                PopulateCharacterList(data);
            }

            //If there are not enough will add random ones
            for (int i = CharacterList.Count(); i < MaxNumberPartyCharacters; i++)
            {
                PopulateCharacterList(Helpers.RandomPlayerHelper.GetRandomCharacter(1));
            }

            return true;
        }
    }
}