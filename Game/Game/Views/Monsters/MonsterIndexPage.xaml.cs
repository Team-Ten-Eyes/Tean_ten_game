using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{

    public partial class MonsterIndexPage : ContentPage
    {
        readonly MonsterViewModel ViewModel;
        /// <summary>
        /// Base Constructor for item index page
        /// </summary>
        public MonsterIndexPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = MonsterViewModel.Instance;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            BaseMonster data = args.SelectedItem as BaseMonster;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new MonsterReadPage(new GenericViewModel<BaseMonster>(data)));

            //// Manually deselect item.
            MonsterListView.SelectedItem = null;
        }
        /// <summary>
        /// Button helped for monster add. Pushes a monal monster create to the navigation stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddMonster_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterCreatePage(new GenericViewModel<BaseMonster>())));
        }


        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (ViewModel.Dataset.Count == 0)
            {
                ViewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (ViewModel.NeedsRefresh())
            {
                ViewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = ViewModel;
        }

    }
}