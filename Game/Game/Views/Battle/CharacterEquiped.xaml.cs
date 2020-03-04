using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
using Game.Services;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterEquiped : ContentPage
    {

        //a gineric view model of the character in the party
        readonly GenericViewModel<BaseCharacter> ViewModel;

        // This uses the Instance so it can be shared with other Battle Pages as needed
        //using this to equip items from the database
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        public CharacterEquiped(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();
            BindingContext = this.ViewModel = data;
            ItemListView.ItemsSource = EngineViewModel.DatabaseItemList;
        }

        public void On_items_selected(object sender, SelectedItemChangedEventArgs args)
        {
            DisplayAlert("Attack!!!", "Attack !!!", "OK");
        }
    }
}