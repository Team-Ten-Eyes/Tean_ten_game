using Game.Models;
using System;
using System.Collections.ObjectModel;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class BattleEngineViewModel
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile BattleEngineViewModel instance;
        private static readonly object syncRoot = new Object();

        public static BattleEngineViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new BattleEngineViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton


        /// <summary>
        /// The Battle Engine
        /// </summary>
        public Engine.BattleEngine Engine = new Engine.BattleEngine();

        // Hold the Proposed List of Characters for the Battle to Use
        public ObservableCollection<BaseCharacter> PartyCharacterList { get; set; } = new ObservableCollection<BaseCharacter>();

        // Hold the View Model to the CharacterIndexViewModel
        public CharacterViewModel DatabaseCharacterViewModel = CharacterViewModel.Instance;

        // Have the Database Character List point to the Character View Model List
        public ObservableCollection<BaseCharacter> DatabaseCharacterList { get; set; } = CharacterViewModel.Instance.Dataset;

        //Have the database of the items for when you equip the characters
        public ObservableCollection<ItemModel> DatabaseItemList { get; set; } = ItemIndexViewModel.Instance.Dataset;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BattleEngineViewModel()
        {
        }

        #endregion Constructor
    }
}