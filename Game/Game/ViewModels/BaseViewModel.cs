using Game.Models;
using Game.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Game.ViewModels
{
    /// <summary>
    /// Base View Model for Data
    /// </summary>
    public class BaseViewModel<T> : INotifyPropertyChanged where T : new()
    {

        // The Mock DataStore
        private IDataStore<T> DataStoreMock => new MockDataStore<T>();
        
        // The SQL DataStore
        private IDataStore<T> DataStoreSQL => new DatabaseService<T>();

        // Which DataStore to use
        public IDataStore<T> DataStore;

        public BaseViewModel()
        {
            SetDataStore(DataStoreEnum.Mock);
            
            // For testing, startup in sql mode
            SetDataStore(DataStoreEnum.SQL);
        }

        // Establish the type of data store to use
        public void SetDataStore(DataStoreEnum data)
        {
            switch (data)
            {
                case DataStoreEnum.SQL:
                    DataStore = DataStoreSQL;
                    break;

                default:
                case DataStoreEnum.Mock:
                    DataStore = DataStoreMock;
                    break;
            }
        }

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
        #endregion
    }
}