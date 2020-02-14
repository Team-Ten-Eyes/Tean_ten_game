
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

        /// <summary>
        /// Constructor for Delete takes GenericViewModel<BaseMonster>
        /// </summary>
        public MonsterDeletePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();
            BindingContext = this.viewModel = data;
        }

        /// <summary>
        /// Event handler for button click to cancel
        /// </summary>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(false);
        }
        /// <summary>
        /// Event handler for delete button interaction triggering delete from BaseMonster
        /// </summary>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            await Navigation.PopModalAsync();
        }
    }
}