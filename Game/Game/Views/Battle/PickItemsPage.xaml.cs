using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickItemsPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage()
        {
            InitializeComponent();

            // Add Players to Display
            
            PartyListView.ItemsSource = EngineViewModel.PartyCharacterList;




        }
        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();

            BindingContext = EngineViewModel;
        }


        /// <summary>
        /// Jump to the character equiped page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void select_fighting_character(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterEquiped());
        }

        /// <summary>
        /// This will go to battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Go_to_battle(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattlePage());
        }

    }
}