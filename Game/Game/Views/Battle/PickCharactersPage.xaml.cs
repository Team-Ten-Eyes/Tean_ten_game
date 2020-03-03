using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using Game.Models;
using Game.ViewModels;
using System.Linq;
namespace Game.Views
{
    /// <summary>
    /// Selecting Characters for the Game
    /// 
    /// TODO: Team
    /// our games also allows duplicates and it only allows 5 characters per battle.
    /// 
    /// Instead of using the database list directly make a copy of it in the viewmodel
    /// Then have on select of the database remove the character from that list and add to the part list
    /// Have select from the party list remove it from the party list and add it to the database list
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        // Empty Constructor for UTs
        // public PickCharactersPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public PickCharactersPage()
        {
            InitializeComponent();

            BindingContext = EngineViewModel;
            

            // Clear the Database List and the Party List to start
            EngineViewModel.PartyCharacterList.Clear();

            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnDatabaseCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            BaseCharacter data = args.SelectedItem as BaseCharacter;
            if (data == null)
            {
                return;
            }

            // Manually deselect Character.
            CharactersListView.SelectedItem = null;

            // Don't add more than the party max
            if (EngineViewModel.PartyCharacterList.Count() < EngineViewModel.Engine.MaxNumberPartyCharacters)
            {
                EngineViewModel.PartyCharacterList.Add(data);
            }

            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPartyCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            BaseCharacter data = args.SelectedItem as BaseCharacter;
            if (data == null)
            {
                return;
            }

            // Manually deselect Character.
            PartyListView.SelectedItem = null;

            // Remove the character from the list
            EngineViewModel.PartyCharacterList.Remove(data);

            UpdateNextButtonState();
        }

        /// <summary>
        /// Next Button is based on the count
        /// 
        /// If no selected characters, disable
        /// 
        /// Show the Count of the party
        /// 
        /// </summary>
        public void UpdateNextButtonState()
        {
            // If no characters disable Next button
            BeginBattleButton.IsEnabled = true;

            var currentCount = EngineViewModel.PartyCharacterList.Count();
            if (currentCount == 0)
            {
                BeginBattleButton.IsEnabled = false;
            }

            PartyCountLabel.Text = currentCount.ToString();
        }

        /// <summary>
        /// Jump to the Battle
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleButton_Clicked(object sender, EventArgs e)
        {
            CreateEngineCharacterList();

            await Navigation.PushModalAsync(new NavigationPage(new PickItemsPage()));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Clear out the old list and make the new list
        /// </summary>
        public void CreateEngineCharacterList()
        {
            // Clear the currett list
            EngineViewModel.Engine.CharacterList.Clear();

            // Load the Characters into the Engine
            foreach (var data in EngineViewModel.PartyCharacterList)
            {
                EngineViewModel.Engine.CharacterList.Add(new PlayerInfoModel(data));
            }
        }
    }
}