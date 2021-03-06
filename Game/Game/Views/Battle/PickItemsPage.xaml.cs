﻿using Game.Models;
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
        /// This will go to battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Go_to_battle(object sender, EventArgs e)
        {

           

            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            
            //await Navigation.PopAsync();
            
        }

        /// <summary>
        /// go to that characters equiped page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            PlayerInfoModel data = args.SelectedItem as PlayerInfoModel;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushModalAsync(new BattleEquip(data));

            //// Manually deselect item.
            PartyListView.SelectedItem = null;
        }


    }
}