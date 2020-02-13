
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms.Xaml;

namespace Game.Views
{

    public partial class MonsterDeletePage : ContentPage
    {
        GenericViewModel<BaseMonster> viewModel;


        public MonsterDeletePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();
            BindingContext = this.viewModel = data;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(false);
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            await Navigation.PopModalAsync();
        }
    }
}