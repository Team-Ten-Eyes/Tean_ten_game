using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.Helpers;
using Game.ViewModels;
using System.Collections.Generic;


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
            List<string> strengths = StrengthWeaknessHelper.getMonsterStrengths(ViewModel.Data.Attribute.ToString());
            StrengthListView.ItemsSource = strengths;
        }


        /// <summary>
        /// Update clicked will redirect the user to the upate page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(new GenericViewModel<BaseMonster>(ViewModel.Data))));
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

 
    }
}