﻿using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundOverPage : ContentPage
    {



        //TODO: Add Item Descriptor Popup


        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
        /// <summary>
        /// Constructor
        /// </summary>
        public RoundOverPage()
        {
            InitializeComponent();
            ItemListView.ItemsSource = EngineViewModel.Engine.ItemPool;
        }

        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void PickItems_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PickItemsPage());
        }
        public void On_items_selected(object sender, SelectedItemChangedEventArgs args)
        {
            DisplayAlert("Attack!!!", "Attack !!!", "OK");
        }

        public void ClearItems(object sender, EventArgs e)
        {
            EngineViewModel.Engine.ItemPool.Clear();
        }
    }
}