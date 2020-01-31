using Game.Services;
using Game.Models;
using Game.Views;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace Game.ViewModels
{
    /// <summary>
    /// Index View Model
    /// Manages the list of data records
    /// </summary>
    public class ItemIndexViewModel : BaseViewModel<ItemModel>
    {
        #region Singleton
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile ItemIndexViewModel instance;
        private static readonly object syncRoot = new Object();

        public static ItemIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ItemIndexViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Attributes
        // The Data set of records
        public ObservableCollection<ItemModel> Dataset { get; set; }

        private IDataStore<ItemModel> DataSource_Mock => new MockDataStore<ItemModel>();
        
        private IDataStore<ItemModel> DataSource_SQL => new DatabaseService<ItemModel>();

        public new IDataStore<ItemModel> DataStore;

        // What is the current data source, SQL, Mock
        public int CurrentDataSource = 0;

        // track if the system needs refreshing
        private bool _needsRefresh;

        // Track if the system has been initialized
        private static bool IsAlreadyInitialized = false;

        // Command to force a Load of data
        public Command LoadDatasetCommand { get; set; }
        #endregion Attributes

        #region Constructor
        /// <summary>
        /// Constructor
        /// 
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public ItemIndexViewModel()
        {
            Title = "Items";

            SetDataSource(CurrentDataSource);   // Set to Mock to start with

            Dataset = new ObservableCollection<ItemModel>();
            LoadDatasetCommand = new Command(async () => await ExecuteLoadDataCommand());

            #region Messages
            // Register the Create Message
            MessagingCenter.Subscribe<ItemCreatePage, ItemModel>(this, "Create", async (obj, data) =>
            {
                await CreateAsync(data as ItemModel);
            });

            // Register the Update Message
            MessagingCenter.Subscribe<ItemUpdatePage, ItemModel>(this, "Update", async (obj, data) =>
            {
                await UpdateAsync(data as ItemModel);
            });

            // Register the Delete Message
            MessagingCenter.Subscribe<ItemDeletePage, ItemModel>(this, "Delete", async (obj, data) =>
            {
                await DeleteAsync(data as ItemModel);
            });

            // Register the Set Data Source Message
            MessagingCenter.Subscribe<AboutPage, int>(this, "SetDataSource", (obj, data) =>
            {
                SetDataSource(data);
            });

            // Register the Wipe Data List Message
            MessagingCenter.Subscribe<AboutPage, bool>(this, "WipeDataList", (obj, data) =>
            {
                WipeDataList();
            });

            #endregion Messages

            // Load the data sets
            LoadDefaultData();
        }
        #endregion Constructor

        #region DataSourceManagement

        /// <summary>
        /// Loads the Default Data set
        /// </summary>
        public void LoadDefaultData()
        {
            Task.Run(() => LoadDefaultDataAsync()).Wait();
        }

        /// <summary>
        /// Wipes the current Data from the Data Store
        /// </summary>
        public void WipeDataList()
        {
            DataStore.WipeDataList();
            SetNeedsRefresh(true);
        }

        /// <summary>
        /// Sets the DataSource to use (SQL or Mock)
        /// </summary>
        /// <param name="isSQL"></param>
        /// <returns></returns>
        public bool SetDataSource(int isSQL)
        {
            if (isSQL == 1)
            {
                DataStore = DataSource_SQL;
                CurrentDataSource = 1;
            }
            else
            {
                DataStore = DataSource_Mock;
                CurrentDataSource = 0;
            }

            // Set Flag for Refresh
            SetNeedsRefresh(true);

            return true;
        }
        #endregion DataSourceManagement

        #region DataOperations_CRUDi
        /// <summary>
        /// API to add the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel data)
        {
            Dataset.Add(data);
            var result = await DataStore.CreateAsync(data);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Get the data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            var myData = await DataStore.ReadAsync(id);
            return myData;
        }

        /// <summary>
        /// Update the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemModel data)
        {
            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(data.Id);
            if (record == null)
            {
                return false;
            }

            // Have the item update itself
            record.Update(data);

            // Save the change to the Data Store
            var result = await DataStore.UpdateAsync(record);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Delete the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(ItemModel data)
        {
            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(data.Id);
            if (record == null)
            {
                return false;
            }

            // remove the record from the current data set in the viewmodel
            Dataset.Remove(data);

            // Have the record deleted from the data source
            var result = await DataStore.DeleteAsync(record.Id);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Having this at the ViewModel, because it has the DataStore
        /// That allows the feature to work for both SQL and the Mock datastores...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateUpdateAsync(ItemModel data)
        {
            // Check to see if the data exist
            var oldData = await ReadAsync(data.Id);
            if (oldData == null)
            {
                await CreateAsync(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync(data);
            if (UpdateResult)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method is for the game engine to call to add an item to the item list
        /// It is not async, so it can be called from the game engine on it's thread
        /// It sets the needs refresh flag
        /// Items added to the list this way, are not saved to the DB, they are temporary during the game.
        /// Refactor for the future would be to create a separate item list for the game to add to, and work with.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Create_Sync(ItemModel data)
        {
            Dataset.Add(data);
            SetNeedsRefresh(true);
            return true;
        }

        /// <summary>
        /// Returns the item passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ItemModel CheckIfItemExists(ItemModel data)
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
        #endregion DataOperations_CRUDi

        #region Refresh
        /// <summary>
        ///  Load the DefaultData
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadDefaultDataAsync()
        {
            if (IsAlreadyInitialized)
            {
                return false;
            }

            IsAlreadyInitialized = true;

            foreach (var item in DefaultData.LoadItems())
            {
                await CreateAsync(item);
            }

            return true;
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        // Command that Loads the Data
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.IndexAsync();

                // Example of how to sort the database output using a linq query.
                // Sort the list
                dataset = dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ToList();

                foreach (var data in dataset)
                {
                    // Make a Copy of the Item Model to add to the List
                    Dataset.Add(new ItemModel(data));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Force data to refresh
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDatasetCommand.CanExecute(null);
            LoadDatasetCommand.Execute(null);
        }
        #endregion Refresh
    }
}