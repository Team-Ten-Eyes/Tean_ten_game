using System.Collections.Generic;
using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// Holds the Data Structures for the Battle Engine
    /// </summary>
    public class BaseEngine
    {
        #region Properties

        // Holds the official ScoreModel
        public ScoreModel BattleScore = new ScoreModel();

        // Holds the Battle Messages as they happen
        public BattleMessagesModel BattleMessagesModel = new BattleMessagesModel();

        // The Pool of items collected during the round as turns happen
        public List<ItemModel> ItemPool = new List<ItemModel>();

        // List of Monsters
        public List<PlayerInfoModel> MonsterList = new List<PlayerInfoModel>();

        // List of Characters
        public List<PlayerInfoModel> CharacterList = new List<PlayerInfoModel>();

        // Current Player who is the attacker
        public PlayerInfoModel CurrentAttacker;

        // Current Player who is the Defender
        public PlayerInfoModel CurrentDefender;

        // Hold the list of players (MonsterModel, and character by guid), and order by speed
        public List<PlayerInfoModel> PlayerList = new List<PlayerInfoModel>();

        // Current Round State
        public RoundEnum RoundStateEnum = RoundEnum.Unknown;

        // Max Number of Characters
        public int MaxNumberPartyCharacters = 5;

        // Max Number of Monsters
        public int MaxNumberPartyMonsters = 6;

        // Max Number of Rounds for AutoBattle
        public int MaxRoundCount = 1000;

        // Max Number of Turns for AutoBattle
        public int MaxTurnCount = 10000;

        #endregion Properties
    }
}