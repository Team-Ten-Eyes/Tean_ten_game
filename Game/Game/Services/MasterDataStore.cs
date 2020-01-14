using Game.ViewModels;
using Game.Models;

namespace Game.Services
{
    public static class MasterDataStore
    {
        // Holds which datastore to use.

        private static DataStoreEnum _dataStoreEnum = DataStoreEnum.Mock;

        // Returns which dtatstore to use
        public static DataStoreEnum GetDataStoreMockFlag()
        {
            return _dataStoreEnum;
        }

        // Switches the datastore values.
        // Loads the databases...
        public static void ToggleDataStore(DataStoreEnum dataStoreEnum)
        {
            switch (dataStoreEnum)
            {

                case DataStoreEnum.Mock:
                    _dataStoreEnum = DataStoreEnum.Mock;
                    ItemIndexViewModel.Instance.SetDataStore(DataStoreEnum.Mock);

                    break;

                case DataStoreEnum.SQL:
                default:
                    _dataStoreEnum = DataStoreEnum.SQL;
                    ItemIndexViewModel.Instance.SetDataStore(DataStoreEnum.SQL);
                    break;
            }

            // Load the Data
            ForceDataRestoreAll();
        }

        // Force all modes to load data...
        public static void ForceDataRestoreAll()
        {
            ItemIndexViewModel.Instance.SetNeedsRefresh(true);
        }
    }
}
