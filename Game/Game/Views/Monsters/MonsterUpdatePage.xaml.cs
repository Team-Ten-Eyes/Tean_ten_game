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
    public partial class MonsterUpdatePage : ContentPage
    {

        readonly GenericViewModel<BaseMonster> ViewModel;
        public MonsterUpdatePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
        }


        /// <summary>
        /// will sage the monster to the index list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.Data.Name.Length > 12)
                await DisplayAlert("Name Too Long", "Must Be Less Than 13 Chars", "OK");
            else {
                MessagingCenter.Send(this, "Update", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }


        /// <summary>
        /// this disables the back button for android becuase
        /// the page is modal
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}