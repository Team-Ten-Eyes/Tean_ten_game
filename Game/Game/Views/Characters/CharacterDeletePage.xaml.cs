
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms.Xaml;

namespace Game.Views
{

    public partial class CharacterDeletePage : ContentPage
    {
        CharacterViewModel viewModel;

        public CharacterDeletePage(CharacterViewModel data)
        {
            InitializeComponent();
            BindingContext = this.viewModel = data;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("su", "cancle button clicked", "ok");
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("su", "Cancle Button clicked", "ok");
        }
    }
}