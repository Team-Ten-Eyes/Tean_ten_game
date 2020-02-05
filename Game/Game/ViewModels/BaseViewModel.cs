using Game.Models;
using Game.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    /// Base View Model for Data
    /// </summary>
    public class BaseViewModel<T> : INotifyPropertyChanged where T : new()
    {
        #region Attributes

        // The Mock DataStore
        private IDataStore<T> DataSource_Mock => new MockDataStore<T>();

        // The SQL DataStore
        private IDataStore<T> DataSource_SQL => new DatabaseService<T>();

        // Which DataStore to use
        public IDataStore<T> DataStore;

        // The Data set of records
        public ObservableCollection<T> Dataset { get; set; }

        // Tack the current data source, SQL, Mock
        public int CurrentDataSource = 0;

        // Track if the system needs refreshing
        public bool _needsRefresh;

        // Command to force a Load of data
        public Command LoadDatasetCommand { get; set; }

        /// <summary>
        /// Mark if the view model is busy loading or done loading
        /// </summary>
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        /// <summary>
        /// The String to show on the page
        /// </summary>
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #endregion Attributes

        #region Constructor

        /// <summary>
        /// Defualt Constructor
        /// </summary>
        public BaseViewModel()
        {
        }

        /// <summary>
        /// Initialize the ViewModel
        /// Sets the collection Dataset
        /// Sets the Load command
        /// Sets the default data source
        /// </summary>
        public async void Initialize()
        {
            Dataset = new ObservableCollection<T>();
            LoadDatasetCommand = new Command(async () => await ExecuteLoadDataCommand());

            await SetDataSource(CurrentDataSource);   // Set to Mock to start with
        }

        #endregion Constructor

        #region DataSourceManagement
        /// <summary>
        /// Sets the DataSource to use (SQL or Mock)
        /// </summary>
        /// <param name="isSQL"></param>
        /// <returns></returns>
        async public Task<bool> SetDataSource(int isSQL)
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

            await LoadDefaultDataAsync();

            // Set Flag for Refresh
            SetNeedsRefresh(true);

            return await Task.FromResult(true);
        }

        /// <summary>
        ///  Load the DefaultData
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadDefaultDataAsync()
        {
            // If data exists, do not run
            if (Dataset.Count>0)
            {
                return false;
            }

            // Take all the items and add them if they don't already exist
            foreach (var data in GetDefaultData())
            {
                await CreateUpdateAsync(data);
            }

            return true;
        }

        /// <summary>
        /// Get the Default Data
        /// The ViewModel will Implement
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetDefaultData()
        {
            return new List<T>();
        }

        #endregion DataSourceManagement

        #region Refresh

        /// <summary>
        /// Command that Loads the Data 
        /// </summary>
        /// <returns></returns>
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
                dataset = SortDataset(dataset);

                foreach (var data in dataset)
                {
                    // Make a Copy of the Item Model to add to the List
                    Dataset.Add(data);
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
        /// Default Sort Dataset
        /// the default just returns the list
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public virtual List<T> SortDataset(List<T> dataset)
        {
            return dataset
                .ToList();
        }

        /// <summary>
        /// Return True if a refresh is needed 
        /// It sets the refresh flag to false
        /// </summary>
        /// <returns></returns>
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the need to refresh 
        /// </summary>
        /// <param name="value"></param>
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
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
        
        #region DataSourceManagement

        /// <summary>
        /// Wipes the current Data from the Data Store
        /// </summary>
        public async Task<bool> WipeDataListAsync()
        {
            await DataStore.WipeDataListAsync();

            // Load the Sample Data
            await LoadDefaultDataAsync();

            SetNeedsRefresh(true);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Returns the current data source
        /// </summary>
        public int GetCurrentDataSource()
        {
            return CurrentDataSource;
        }

        #endregion DataSourceManagement

        #region DataOperations_CRUDi

        /// <summary>
        /// API to add the Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(T data)
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
        public async Task<T> ReadAsync(string id)
        {
            var myData = await DataStore.ReadAsync(id);
            return myData;
        }

        /// <summary>
        /// Update the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T data)
        {
            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(((BaseModel<T>)(object)data).Id);
            if (record == null)
            {
                return false;
            }

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
        public async Task<bool> DeleteAsync(T data)
        {
            // Check that the record exists, if it does not, then exit with false
            var record = await ReadAsync(((BaseModel<T>)(object)data).Id);
            if (record == null)
            {
                return false;
            }

            // remove the record from the current data set in the viewmodel
            Dataset.Remove(data);

            // Have the record deleted from the data source
            var result = await DataStore.DeleteAsync(((BaseModel<T>)(object)record).Id);

            SetNeedsRefresh(true);

            return result;
        }

        /// <summary>
        /// Having this at the ViewModel, because it has the DataStore
        /// That allows the feature to work for both SQL and the Mock datastores...
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateUpdateAsync(T data)
        {
            // Check to see if the data exist
            var oldData = await ReadAsync(((BaseModel<T>)(object)data).Id);
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
        public bool Create_Sync(T data)
        {
            Dataset.Add(data);
            SetNeedsRefresh(true);
            return true;
        }

        #endregion DataOperations_CRUDi

        #region PropertyChanges

        /// <summary>
        /// Tracking what has changed in the dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore,
            T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion PropertyChanges

        #region INotifyPropertyChanged
        
        /// <summary>
        /// Notify when changes happen
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}