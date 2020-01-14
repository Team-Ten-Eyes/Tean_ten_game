using System;
using System.ComponentModel;
using Xamarin.Forms;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    /// <summary>
    /// Index Page
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    public partial class ItemIndexPage : ContentPage
    {
        // The view model, used for data binding
        readonly ItemIndexViewModel viewModel;

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public ItemIndexPage()
        {
            InitializeComponent();

            BindingContext = viewModel = ItemIndexViewModel.Instance;
        }


        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel item = args.SelectedItem as ItemModel;
            if (item == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new ItemReadPage(new ItemViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemCreatePage(new ItemViewModel())));
        }

        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then reload the data
            if (viewModel.Dataset.Count == 0)
            {
                viewModel.LoadDatasetCommand.Execute(null);
            }

            if (viewModel.NeedsRefresh())
            {
                viewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = viewModel;
        }
    }
}