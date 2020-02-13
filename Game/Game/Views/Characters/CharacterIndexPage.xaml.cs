using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;

namespace Game.Views 
{
   
    public partial class CharacterIndexPage : ContentPage
    {
        readonly CharacterViewModel ViewModel;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddCharacter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterCreatePage(new GenericViewModel<BaseCharacter>())));
        }

        ///// <summary>
        ///// Call to Add a new record
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new ItemCreatePage(new GenericViewModel<ItemModel>())));
        //}

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