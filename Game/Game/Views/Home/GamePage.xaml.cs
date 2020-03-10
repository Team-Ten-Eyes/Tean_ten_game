using Game.Engine;
using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GamePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Jump to the Dungeon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void DungeonButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonstersInTheNextBattle());
        }

        /// <summary>
        /// Jump to the Village
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void VillageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VillagePage());
        }

        /// <summary>
        /// Jump to the Dungeon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AutobattleButton_Clicked(object sender, EventArgs e)
        {
            // Run the Autobattle simulation from here
            AutoBattleEngine AutoEngine = new AutoBattleEngine();

            var catchResult = await AutoEngine.RunAutoBattle();

            ScoreModel endScore = AutoEngine.GetScoreObject();




            // Call to the Score Page

            await Navigation.PushModalAsync(new NavigationPage(new ScorePage(new GenericViewModel<ScoreModel>(endScore))));
        }
    }
}