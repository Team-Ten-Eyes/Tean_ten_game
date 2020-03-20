
using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace Game.Views
{

    public partial class CharacterReadPage : ContentPage
    {
        public readonly GenericViewModel<BaseCharacter> ViewModel;
        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public CharacterReadPage(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
            double hp = ((double)ViewModel.Data.CurrHealth) / ((double)ViewModel.Data.MaxHealth);
            double mana = (((double)ViewModel.Data.Mana) / ((double)ViewModel.Data.MaxMana));
            HPbar.Progress = hp;
            Manabar.Progress = mana;
            List<string> strengths = StrengthWeaknessHelper.getCharacterStrengths(ViewModel.Data.Attribute.ToString());
            StrengthListView.ItemsSource = strengths;
        }






        /// <summary>
        /// Button helper for update which adds an update page to the modal stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterUpdatePage(new GenericViewModel<BaseCharacter>(ViewModel.Data))));
            await Navigation.PopAsync();
        }



        // <summary>
        // Calls for Delete
        // </summary>
        // <param name = "sender" ></ param >
        // < param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterDeletePage(new GenericViewModel<BaseCharacter>(ViewModel.Data))));
            await Navigation.PopAsync();
        }
    }

}

