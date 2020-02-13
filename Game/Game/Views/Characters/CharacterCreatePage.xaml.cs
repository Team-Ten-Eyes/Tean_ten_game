﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterCreatePage : ContentPage
    {
        GenericViewModel<BaseCharacter> ViewModel { get; set; }
        public CharacterCreatePage(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();

            data.Data = new BaseCharacter();

            BindingContext = this.ViewModel = data;
        }

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
           MessagingCenter.Send(this, "Create", ViewModel.Data);
           await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}