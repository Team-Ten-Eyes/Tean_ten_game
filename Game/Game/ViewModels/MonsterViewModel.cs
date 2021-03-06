﻿using Game.Models;
using Game.Services;
using Game.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;


namespace Game.ViewModels
{   /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>

    public class MonsterViewModel : BaseViewModel<BaseMonster>
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile MonsterViewModel instance;
        private static readonly object syncRoot = new Object();

        public BaseMonster Data { get; set; }

        public static MonsterViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MonsterViewModel();
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
        public MonsterViewModel(BaseMonster data = null)
        {
            Title = data?.Name;
            Data = data;
        }
        public MonsterViewModel()
        {
            Title = "Monsters";

            #region Messages

            // Register the Create Message
            MessagingCenter.Subscribe<MonsterCreatePage, BaseMonster>(this, "Create", async (obj, data) =>     //NEED TO CHANGE THIS
            {
                await CreateAsync(data as BaseMonster);
            });

            //Register the Update Message
            MessagingCenter.Subscribe<MonsterUpdatePage, BaseMonster>(this, "Update", async (obj, data) =>
            {
                // Have the item update itself
                data.Update(data);

                await UpdateAsync(data as BaseMonster);
            });

            //Register the Delete Message
            MessagingCenter.Subscribe<MonsterDeletePage, BaseMonster>(this, "Delete", async (obj, data) =>  //NEED TO CHANGE THIS
            {
                await DeleteAsync(data as BaseMonster);
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
        /// Returns the item passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BaseMonster CheckIfItemExists(BaseMonster data)
        {
            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item...

            var myList = Dataset.Where(a =>
                                        a.Name == data.Name)
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
        public override List<BaseMonster> GetDefaultData()
        {
            return DefaultData.LoadData(new BaseMonster());
        }

        #endregion DataOperations_CRUDi


    }

}
