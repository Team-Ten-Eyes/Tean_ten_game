using Game.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Game.Views;

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
        public ObservableCollection<PlayerInfoModel> BattleMonsterList { get; set; } = new ObservableCollection<PlayerInfoModel>();

        public ObservableCollection<PotionsModel> Potions { get; set; } = new ObservableCollection<PotionsModel>();
        // Hold the View Model to the CharacterIndexViewModel
        public CharacterViewModel DatabaseCharacterViewModel = CharacterViewModel.Instance;

        // Have the Database Character List point to the Character View Model List
        public ObservableCollection<BaseCharacter> DatabaseCharacterList { get; set; } = CharacterViewModel.Instance.Dataset;

        //Have the database of the items for when you equip the characters
        public ObservableCollection<ItemModel> DatabaseItemList { get; set; } = ItemIndexViewModel.Instance.Dataset;


        #region Constructor

        ///// <summary>
        ///// Constructor
        ///// </summary>
        //public BattleEngineViewModel()
        //{
        //}

        #endregion Constructor

        #region Hack messages


        /// <summary>
        /// Sets the roundHealingEnum to on and off
        /// </summary>
        /// <param name="isOn"></param>
        /// <returns></returns>
        public bool SetRoundHealing(int isOn)
        {
            if (isOn == 1)
            {
                Engine.RoundHealing = RoundHealingEnum.Healing_off;
                return true;
            }
            else if (isOn == 2)
            {
                Engine.RoundHealing = RoundHealingEnum.Healing_on;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Toggles Speed 20% chance
        /// </summary>
        /// <param name="isOn"></param>
        /// <returns></returns>
        public bool setSpeed_20(int isOn)
        {
            if (isOn == 1)
            {
                Engine.Speed20 = false;
                return true;
            }
            else if (isOn == 2)
            {
                Engine.Speed20 = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// toggles speed always for testing
        /// </summary>
        /// <param name="isOn"></param>
        /// <returns></returns>
        public bool SetSpeedAlways(int isOn)
        {
            if (isOn == 1)
            {
                Engine.SpeedAlways = false;
                return true;
            }
            else if (isOn == 2)
            {
                Engine.SpeedAlways = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// has the toggle to allow boss battles or not
        /// </summary>
        /// <param name="isOn"></param>
        /// <returns></returns>
        public bool SetBossBattle(bool isOn)
        {
            if(isOn)
            {
                Engine.BossBattleFunctionality = true;
                return true;
            }
            else
            {
                Engine.BossBattleFunctionality = false;
                return true;
            }
            return false;
        }



        #endregion Hack messages
    }
}