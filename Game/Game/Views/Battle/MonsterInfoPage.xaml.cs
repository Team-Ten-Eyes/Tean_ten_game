using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.Helpers;
using Game.ViewModels;



namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterInfoPage : ContentPage
    {
        readonly GenericViewModel<BaseMonster> ViewModel;
        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public MonsterInfoPage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
            List<string> strengths = StrengthWeaknessHelper.getMonsterStrengths(ViewModel.Data.Attribute.ToString());
            StrengthListView.ItemsSource = strengths;
        }


        /// <summary>
        /// Update clicked will redirect the user to the upate page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(new GenericViewModel<BaseMonster>(ViewModel.Data))));
            await Navigation.PopAsync();
        }



        // <summary>
        // Calls for Delete
        // </summary>
        // <param name = "sender" ></ param >
        // < param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterDeletePage(new GenericViewModel<BaseMonster>(ViewModel.Data))));
            await Navigation.PopAsync();
        }
    }
}