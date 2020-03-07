using Game.Models;
using Game.Services;
using Game.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class CharacterViewModel : BaseViewModel<BaseCharacter>
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile CharacterViewModel instance;
        private static readonly object syncRoot = new Object();

        public static CharacterViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CharacterViewModel();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public CharacterViewModel()
        {
            Title = "Characters";

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<CharacterCreatePage, BaseCharacter>(this, "Create", async (obj, data) =>
            {
                await CreateAsync(data as BaseCharacter);
            });

            // Register the Update Message
            MessagingCenter.Subscribe<CharacterUpdatePage, BaseCharacter>(this, "Update", async (obj, data) =>
            {
                // Have the Character update itself
                data.Update(data);

                await UpdateAsync(data as BaseCharacter);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<CharacterDeletePage, BaseCharacter>(this, "Delete", async (obj, data) =>
            {
                await DeleteAsync(data as BaseCharacter);
            });

            // Register the Set Data Source Message
            MessagingCenter.Subscribe<AboutPage, int>(this, "SetDataSource", async (obj, data) =>
            {
                await SetDataSource(data);
            });

            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<AboutPage, bool>(this, "WipeDataList", async (obj, data) =>
            {
                await WipeDataListAsync();
            });

            #endregion Messages
        }

        #endregion Constructor

        #region DataOperations_CRUDi

        /// <summary>
        /// Returns the Character passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override BaseCharacter CheckIfExists(BaseCharacter data)
        {
            // This will walk the Characters and find if there is one that is the same.
            // If so, it returns the Character...

            var myList = Dataset.Where(a =>
                                        a.Name == data.Name &&
                                        a.Description == data.Description &&
                                        a.Level == data.Level &&
                                        a.Experience == data.Experience &&
                                        a.MaxHealth == data.MaxHealth &&
                                        a.CurrHealth == data.CurrHealth
                                        )
                                        .FirstOrDefault();

            if (myList == null)
            {
                // it's not a match, return false;
                return null;
            }

            return myList;
        }

        /// <summary>
        /// Load the Default Data
        /// </summary>
        /// <returns></returns>
        public override List<BaseCharacter> GetDefaultData()
        {
            return DefaultData.LoadData(new BaseCharacter());
        }

        #endregion DataOperations_CRUDi

        #region SortDataSet

        /// <summary>
        /// The Sort Order for the BaseCharacter
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public override List<BaseCharacter> SortDataset(List<BaseCharacter> dataset)
        {
            return dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ThenBy(a => a.Level)
                    .ThenBy(a => a.Experience)
                    .ThenBy(a => a.MaxHealth)
                    .ThenBy(a => a.CurrHealth)
                    .ToList();
        }

        #endregion SortDataSet
    }
}