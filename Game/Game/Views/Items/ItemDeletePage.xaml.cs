using System.ComponentModel;
using Xamarin.Forms;

using Game.ViewModels;
using System;

namespace Game.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ItemDeletePage : ContentPage
    {
        // View Model for Item
        readonly ItemViewModel viewModel;

        // Constructor for Delete takes a view model of what to delete
        public ItemDeletePage(ItemViewModel data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}