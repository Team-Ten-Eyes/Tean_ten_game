
using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;

namespace Game.Views
{

    public partial class CharacterDeletePage : ContentPage
    {
        GenericViewModel<BaseCharacter> viewModel;

        // Empty Constructor for UTs
        public CharacterDeletePage(bool UnitTest) { }

        /// <summary>
        /// Default constructor for the Char Delete Page
        /// </summary>
        public CharacterDeletePage(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();
            BindingContext = this.viewModel = data;
        }

        /// <summary>
        /// Cancel button helper that will pop the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(false);
        }

        /// <summary>
        /// Delete button helper which will remove the data in context from the database in use.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            await Navigation.PopModalAsync();
        }
    }
}