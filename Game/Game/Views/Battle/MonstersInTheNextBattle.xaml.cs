using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonstersInTheNextBattle : ContentPage
    {

        readonly MonsterViewModel ViewModel;
        public MonstersInTheNextBattle()
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
            await Navigation.PushModalAsync(new MonsterInfoPage(new GenericViewModel<BaseMonster>(data)));

            //// Manually deselect item.
            MonsterListView.SelectedItem = null;
        }

        async void Direct_pick_character_page(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PickCharactersPage());
        }
    }
}