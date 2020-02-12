using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;


namespace Game.Views
{
   
    public partial class CharacterReadPage : ContentPage
    {
      readonly GenericViewModel<BaseCharacter> ViewModel;
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
            double hp = ViewModel.Data.CharHealth / ViewModel.Data.MaxHealth;
            double mana = ViewModel.Data.Mana / ViewModel.Data.MaxMana;
            HPbar.Progress = hp;
            Manabar.Progress = mana;
        }

        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterUpdatePage(new CharacterViewModel (ViewModel.Data) )));
            await Navigation.PopAsync();
        }



        // <summary>
        // Calls for Delete
        // </summary>
        // <param name = "sender" ></ param >
        // < param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CharacterDeletePage(new CharacterViewModel(ViewModel.Data))));
            await Navigation.PopAsync();
        }

        //public CharacterReadPage()
        //{
        //    InitializeComponent();
        //    BindingContext = this.ViewModel = data;
        //}
    }
}