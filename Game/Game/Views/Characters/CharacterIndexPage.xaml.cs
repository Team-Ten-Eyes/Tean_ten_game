using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;

namespace Game.Views
{

    public partial class CharacterIndexPage : ContentPage
    {
        readonly CharacterViewModel ViewModel;

        /// <summary>
        /// Constructor for Char Index Page
        /// </summary>
        public CharacterIndexPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = CharacterViewModel.Instance;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            BaseCharacter data = args.SelectedItem as BaseCharacter;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new CharacterReadPage(new GenericViewModel<BaseCharacter>(data)));

            //// Manually deselect item.
            CharacterListView.SelectedItem = null;
        }


        /// <summary>
        /// Button helper for the Add Char button which pushes a new character create page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddCharacter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterCreatePage(new GenericViewModel<BaseCharacter>())));
        }


        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        /// 
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (ViewModel.Dataset.Count == 0)
            {
                ViewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (ViewModel.NeedsRefresh())
            {
                ViewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = ViewModel;
        }

    }
}