using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
       

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterIndexPage : ContentPage
    {
        readonly MonsterViewModel ViewModel;
        public MonsterIndexPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = MonsterViewModel.Instance;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            BaseMonster data = args.SelectedItem as BaseMonster;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
           // await Navigation.PushAsync(new MonsterReadPage(new GenericViewModel<BaseMonster>(data)));

           
            //MonstersListView.SelectedItem = null;
        }
    }
}