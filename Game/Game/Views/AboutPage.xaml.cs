using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// About Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        /// <summary>
        /// Constructor for About Page
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();

            // Turn off Database Settings Frame
            DatabaseSettingsFrame.IsVisible = false;

            // Turn off the Debug Frame
            DebugSettingsFrame.IsVisible = false;
        }

        private void EnableDebugSettings_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.

            DebugSettingsFrame.IsVisible = (e.Value);
        }

        private void DatabaseSettingsSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            DatabaseSettingsFrame.IsVisible = (e.Value);
        }

        private void UseMockDataSourceSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.
            //SetDataSource(e.Value);
        }

        async void ClearDatabase_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Sure you want to Delete All Data, and start over?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables.
               // SQLDataStore.Instance.InitializeDatabaseNewTables();
            }
        }

    }
}