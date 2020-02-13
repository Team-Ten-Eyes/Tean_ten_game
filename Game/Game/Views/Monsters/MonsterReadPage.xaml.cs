using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;


namespace Game.Views
{

    public partial class MonsterReadPage : ContentPage
    {
        readonly GenericViewModel<BaseMonster> ViewModel;
        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public MonsterReadPage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
            double hp = ((double) ViewModel.Data.MonsterHealth )/ ((double) ViewModel.Data.MaxHealth);
            //double mana = ViewModel.Data.Mana / ViewModel.Data.MaxMana;
            HP_bar.Progress = hp;
            //Manabar.Progress = mana;
        }

        async void Update_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(new GenericViewModel<BaseMonster>(ViewModel.Data))));
            await Navigation.PopAsync();
        }



        // <summary>
        // Calls for Delete
        // </summary>
        // <param name = "sender" ></ param >
        // < param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterDeletePage(new GenericViewModel<BaseMonster>(ViewModel.Data))));
            await Navigation.PopAsync();
        }

        //public CharacterReadPage()
        //{
        //    InitializeComponent();
        //    BindingContext = this.ViewModel = data;
        //}
    }
}