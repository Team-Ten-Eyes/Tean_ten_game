using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// About Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        // Constructor for UnitTests
        public AboutPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for About Page
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();

            // Hide the Debug Settings
            DatabaseSettingsFrame.IsVisible = false;

            // Turn off the Settings Frame
            DebugSettingsFrame.IsVisible = false;



            
            // Set to the curent date and time
            CurrentDateTime.Text = DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
        }

        /// <summary>
        /// Show or hide the Database Frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DatabaseSettingsSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Database Section
            DatabaseSettingsFrame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Sow or hide the Debug Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DebugSettingsSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // Show or hide the Debug Settings
            DebugSettingsFrame.IsVisible = (e.Value);
        }

        /// <summary>
        /// Data Source Toggle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataSource_Toggled(object sender, EventArgs e)
        {
            // Flip the settings
            if (DataSourceValue.IsToggled == true)
            {
                MessagingCenter.Send(this, "SetDataSource", 1);
            }
            else
            {
                MessagingCenter.Send(this, "SetDataSource", 0);
            }
        }

        /// <summary>
        /// Button to delete the data store
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void WipeDataList_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Data", "Are you sure you want to delete all data?", "Yes", "No");

            if (answer)
            {
                MessagingCenter.Send(this, "WipeDataList", true);
            }
        }

        public void Allow_Round_Healing( object sender, EventArgs e)
        {
            if (RoundHealingValue.IsToggled == true)
                EngineViewModel.SetRoundHealing(2);
            else
                EngineViewModel.SetRoundHealing(1);
          
        }

        async void Speed_Reversal_Basic(object sender, EventArgs e) {
            if (RoundHealingValue.IsToggled == true)
                EngineViewModel.setSpeed_20(2);
            else
                EngineViewModel.setSpeed_20(1);
        }

        async void Speed_Reversal_Certain(object sender, EventArgs e)
        {
            if (RoundHealingValue.IsToggled == true)
                EngineViewModel.SetSpeedAlways(2);
            else
                EngineViewModel.SetSpeedAlways(1);
        }

        /// <summary>
        /// will switch the boss battle to on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Allow_Boss_Battle(object sender, EventArgs e)
        {
            if (Boss_Battle_Value.IsToggled == true)
                EngineViewModel.SetBossBattle(true);
            else
                EngineViewModel.SetBossBattle(false);
        }
    }
}