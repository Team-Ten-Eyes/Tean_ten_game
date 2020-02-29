using System;
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
            await Navigation.PushAsync(new MonsterReadPage(new GenericViewModel<BaseMonster>(data)));

            //// Manually deselect item.
            MonsterListView.SelectedItem = null;
        }

        async void Direct_pick_character_page(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickCharactersPage());
        }
    }
}