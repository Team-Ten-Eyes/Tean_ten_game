
using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;

namespace Game.Views
{

    public partial class MonsterDeletePage : ContentPage
    {
        GenericViewModel<BaseMonster> viewModel;

        /// <summary>
        /// Base Constructor for Monster Delete Page
        /// </summary>
        /// /// <param name="data"></param>
        public MonsterDeletePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();
            BindingContext = this.viewModel = data;
        }


        /// <summary>
        /// Cancel button will let the user backout of the delete modal page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(false);
        }


        /// <summary>
        /// will fully delete the monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            await Navigation.PopModalAsync();
        }
    }
}