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
    public partial class ScorePage : ContentPage
    {
        ScoreModel endScore { get; set; } = null;


        GenericViewModel<ScoreModel> ViewModel;
        /// <summary>
        /// Constructor
        /// </summary>
        public ScorePage()
        {
            BindingContext = new ScoreModel();

            InitializeComponent();
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        public ScorePage(GenericViewModel<ScoreModel> data)
        {
            BindingContext = this.ViewModel = data;

            InitializeComponent();
        }

        /// <summary>
        /// Close Button Returns User To Monsters Lurking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PushAsync(new GamePage());
            BattleEngineViewModel.Instance.Engine.BattleScore.RoundCount = 0;
            //message center not pushing to scorelist DB
        }
    }
}